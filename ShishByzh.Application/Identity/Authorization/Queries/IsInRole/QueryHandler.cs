using ShishByzh.Application.Common.Interfaces;

namespace ShishByzh.Application.Identity.Authorization.Queries.IsInRole
{
    public class QueryHandler(IIdentityService identityService) : IRequestHandler<Query, bool>
    {
        public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = await identityService.IsInRoleAsync(request.UserId, request.Role);

            return response;
        }
    }
}
