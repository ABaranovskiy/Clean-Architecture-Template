namespace ShishByzh.Application.Identity.Authorization.Queries.GetUserName;

public record Query(Guid UserId) : IRequest<string?>;
