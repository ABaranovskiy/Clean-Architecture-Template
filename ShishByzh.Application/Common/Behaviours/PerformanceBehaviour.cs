using System.Diagnostics;
using Microsoft.Extensions.Logging;
using ShishByzh.Application.Common.Interfaces;

namespace ShishByzh.Application.Common.Behaviours;

public class PerformanceBehaviour<TRequest, TResponse>(
    ILogger<TRequest> logger,
    ICurrentUser currentUser,
    IIdentityService identityService)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly Stopwatch _timer = new();

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next();

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 1000)
        {
            var requestName = typeof(TRequest).FullName;
            var userId = currentUser.Id ?? Guid.Empty;
            
            var userName = string.Empty;

            if (userId != Guid.Empty)
            {
                userName = await identityService.GetUserNameAsync(userId);
            }

            logger.LogWarning("ShishByzh Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                requestName, elapsedMilliseconds, userId, userName, request);
        }

        return response;
    }
}
