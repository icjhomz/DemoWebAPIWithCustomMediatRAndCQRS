using CustomMediatR.Common.Interfaces;
using System.Diagnostics;

namespace DemoWebAPIWithCustomMediatRAndCQRS.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            _logger.LogInformation("Handling {RequestName} with request: {@Request}", typeof(TRequest).Name, request);
            var response = await next();
            stopwatch.Stop();

            _logger.LogInformation("Handled {RequestName} with response: {@Response} in {ElapsedMilliseconds} ms", typeof(TRequest).Name, response, stopwatch.ElapsedMilliseconds);
            return response;
        }
    }
}
