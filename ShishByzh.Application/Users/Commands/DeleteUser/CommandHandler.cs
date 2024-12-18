using ShishByzh.Application.Common.Interfaces;

namespace ShishByzh.Application.Users.Commands.DeleteUser;

public class CommandHandler(IIdentityService identityService) : IRequestHandler<Command, bool>
{
    public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
    {
        var result = await identityService.DeleteUserAsync(request.UserId);
        
        return result.Succeeded;
    }
}