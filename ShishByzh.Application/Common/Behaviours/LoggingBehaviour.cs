using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using ShishByzh.Application.Common.Interfaces;

namespace ShishByzh.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest>(ILogger<TRequest> logger, ICurrentUser user, IIdentityService identityService)
    : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).FullName;
        var userId = user.Id ?? Guid.Empty;
        string? userName = string.Empty;

        if (userId != Guid.Empty)
        {
            userName = await identityService.GetUserNameAsync(userId);
        }

        logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
    }
}