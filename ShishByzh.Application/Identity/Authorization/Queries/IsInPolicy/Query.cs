namespace ShishByzh.Application.Identity.Authorization.Queries.IsInPolicy;

[Authorize]
public record Query(Guid UserId, string Policy) : IRequest<bool>;
