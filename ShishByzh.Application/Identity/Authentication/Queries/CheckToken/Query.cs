using ShishByzh.Application.Common.Models;

namespace ShishByzh.Application.Identity.Authentication.Queries.CheckToken;

public record Query(string Token) : IRequest<Result>;
