using ShishByzh.Application.Common.Interfaces;

namespace ShishByzh.Application.Identity.Authentication.Queries.SignIn
{
    public class QueryHandler(IIdentityService identityService, IMapper mapper) : IRequestHandler<Query, LoginResponse>
    {
        public async Task<LoginResponse> Handle(Query request, CancellationToken cancellationToken)
        {
            var response = await identityService.SignInWithCredentials(request.UserName, request.Password);

            if (!response.Result.Succeeded || response.User == null)
                return new LoginResponse { IsSuccessful = false, Errors = response.Result.Errors, Token = null };

            var mappedUser = mapper.Map<UserDto>(response.User);
            mappedUser.Role = response.Role;
            
            var token = identityService.GenerateToken(response.User.Id, request.UserName, response.Role);
            return new LoginResponse
            {
                User = mappedUser,
                IsSuccessful = true, Token = token, Errors = null
            };
        }
    }
}
