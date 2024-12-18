using ShishByzh.Application.Common.Interfaces;

namespace ShishByzh.Application.Identity.Authentication.Queries.SignOut
{
    public class QueryHandler(IIdentityService identityService) : IRequestHandler<Query>
    {
        public async Task Handle(Query request, CancellationToken cancellationToken)
        {
            await identityService.SignOut();
        }
    }
}
