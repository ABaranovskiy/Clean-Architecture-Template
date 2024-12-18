using ShishByzh.Application.Common.Interfaces;

namespace ShishByzh.Application.Identity.Authorization.Queries.IsInPolicy
{
    public class QueryHandler(IIdentityService identityService) : IRequestHandler<Query, bool>
    {
        public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = await identityService.IsInPolicyAsync(request.UserId, request.Policy);

            return response;
        }
    }
}
