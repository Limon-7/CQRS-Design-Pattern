using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
	public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly ILogger<TRequest> _logger;

		public LoggingBehavior(ILogger<TRequest> logger)
		{
			_logger = logger;
		}
		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			_logger.LogInformation("Calling Logger Handler");
			var requestName = typeof(TRequest).Name;
			var response = await next();
			_logger.LogInformation($"Name: {requestName}  Request: {request} Response: {response}");
			return response;

		}
	}
}
