using Microsoft.Extensions.Logging;

namespace ShishByzh.Application.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).FullName;

            logger.LogError(ex, "ShishByzh Request: Unhandled Exception for Request {Name} {@Request}", 
                requestName, request);

            throw;
        }
    }
}
