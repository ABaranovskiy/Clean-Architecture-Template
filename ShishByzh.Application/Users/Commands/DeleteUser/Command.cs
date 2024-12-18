namespace ShishByzh.Application.Users.Commands.DeleteUser;

[Authorize(Policy="OnlyArchitects")]
public record Command(Guid UserId) : IRequest<bool>;
