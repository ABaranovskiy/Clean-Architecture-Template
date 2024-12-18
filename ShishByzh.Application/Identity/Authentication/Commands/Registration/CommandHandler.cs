using ShishByzh.Application.Common.Interfaces;
using ShishByzh.Domain.Users;

namespace ShishByzh.Application.Identity.Authentication.Commands.Registration;

public class CommandHandler(IIdentityService identityService) : IRequestHandler<Command, Guid>
{
    public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
    {
        var user = new User(request.Fio, request.UserName, null);

        var result = await identityService.CreateUserAsync(user, request.Password, request.Role);
        
        return result.UserId;
    }
}