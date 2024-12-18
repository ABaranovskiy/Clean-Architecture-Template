namespace ShishByzh.Application.Identity.Authorization.Queries.IsInRole;

[Authorize]
public record Query(Guid UserId, string Role) : IRequest<bool>;
