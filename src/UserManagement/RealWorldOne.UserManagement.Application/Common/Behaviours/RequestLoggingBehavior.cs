using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RealWorldOne.UserManagement.Application.Common.Behaviours
{
    public class RequestLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<RequestLoggingBehavior<TRequest, TResponse>> _logger;

        public RequestLoggingBehavior(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<RequestLoggingBehavior<TRequest, TResponse>>();
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                _logger.LogInformation("Processing {Command} command/query", typeof(TRequest).Name);

                var response = await next();

                _logger.LogInformation(
                    "Processed {Command} {Result} command/query result",
                    typeof(TRequest).Name,
                    response.GetType().Name);

                return response;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error occurred while processing {Command}", typeof(TRequest).Name);
                throw;
            }
        }
    }
}