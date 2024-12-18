namespace ShishByzh.Application.Users.Queries.GetUsers;

[Authorize]
public record Query : IRequest<Vm>;