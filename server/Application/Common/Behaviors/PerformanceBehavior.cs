using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Behaviors
{
	public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly Stopwatch _timer;
		private readonly ILogger<TRequest> _logger;

		public PerformanceBehavior(ILogger<TRequest> logger)
		{
			_timer = new Stopwatch();
			_logger = logger;
		}
		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			_timer.Start();
			var response = await next();
			_timer.Stop();
			var elapsedMilliseconds = _timer.ElapsedMilliseconds;
			if (elapsedMilliseconds> 500)
			{
				var requestName = typeof(TRequest).Name;
			_logger.LogWarning($" Long Running Name: {requestName} {elapsedMilliseconds} milliseconds Request:{request} Response: {response} ");
			}
			return response;
		}
	}
}
