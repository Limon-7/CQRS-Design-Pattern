using Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;


namespace Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
			//services.AddScoped(typeof(IPipelineBehavior<,>), typeof(RestExceptionBehavior<,>));
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
			return services;
		}
	}
}
