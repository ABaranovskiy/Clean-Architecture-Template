using ShishByzh.Application.Common.Interfaces;
using ShishByzh.Application.Common.Models;

namespace ShishByzh.Application.Identity.Authentication.Queries.CheckToken
{
    public class QueryHandler(IIdentityService identityService) : IRequestHandler<Query, Result>
    {
        public Task<Result> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = identityService.ValidateToken(request.Token);

            return Task.FromResult(response);
        }
    }
}
