using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APP.API.Options;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace APP.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      // provide httpcontext outside of the project
      services.AddHttpContextAccessor();
      services.AddInfrastructure(Configuration);
      services.AddApplication();
      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog App", Version = "v1" });
      });

      services.AddCors(opt =>
            {
              opt.AddPolicy("CorsPolicy", policy =>
              {
                policy
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials()
                      .WithExposedHeaders("WWW-Authenticate", "Pagination")
                      .WithOrigins("http://localhost:3000");
              });
            });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseHsts();

      }

      #region SwegerOptions...
      var swaggerOptions = new SwaggerOptions();
      Configuration.GetSection(nameof(SwaggerOptions)).Bind(swaggerOptions);
      app.UseSwagger(opt =>
      {
        opt.RouteTemplate = swaggerOptions.JsonRoute;
      });
      app.UseSwaggerUI(c => c.SwaggerEndpoint(swaggerOptions.UIEndpoints, swaggerOptions.Description));
      #endregion

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseCors("CorsPolicy");


      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
