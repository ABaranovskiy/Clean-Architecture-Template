namespace ShishByzh.Application.Users.Queries.GetUser;

[Authorize]
public record Query(Guid UserId) : IRequest<UserDto>;