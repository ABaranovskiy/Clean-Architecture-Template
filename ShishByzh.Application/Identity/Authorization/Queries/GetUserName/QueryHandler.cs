using ShishByzh.Application.Common.Interfaces;

namespace ShishByzh.Application.Identity.Authorization.Queries.GetUserName
{
    public class QueryHandler(IIdentityService identityService) : IRequestHandler<Query, string?>
    {
        public async Task<string?> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = await identityService.GetUserNameAsync(request.UserId);

            return response;
        }
    }
}
