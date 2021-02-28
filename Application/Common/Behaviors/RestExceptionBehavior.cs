using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
	public class RestExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly HttpContext httpContext;
		private readonly IHostingEnvironment _env;

		public RestExceptionBehavior(IHttpContextAccessor httpContextAccessor, IHostingEnvironment env)
		{
			httpContext = httpContextAccessor.HttpContext;
			_env = env;
		}
		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			var _next = await next();
			try
			{
				return _next;
			}
			catch (Exception ex)
			{
				httpContext.Response.ContentType = "application/json";
				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				Console.WriteLine($"Context-type: {httpContext.Response.ContentType} error:{httpContext.Response.StatusCode} Mode:{_env.EnvironmentName}");
				var response = _env.IsDevelopment()
				   ? new RestException(httpContext.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
				   : new RestException(httpContext.Response.StatusCode, "Server Error");

				var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

				var json = JsonSerializer.Serialize(response, options);

				await httpContext.Response.WriteAsync(json);
				return _next;
			}
		}
	}
}
