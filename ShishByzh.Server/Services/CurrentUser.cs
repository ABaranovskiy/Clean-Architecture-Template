using System.Security.Claims;
using ShishByzh.Application.Common.Interfaces;

namespace ShishByzh.Server.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public Guid? Id
    {
        get
        {
            var httpContext = httpContextAccessor.HttpContext;
            var userIdString = httpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userIdString))
            {
                return Guid.Parse(userIdString);
            }
            return null;
        }
    }
}