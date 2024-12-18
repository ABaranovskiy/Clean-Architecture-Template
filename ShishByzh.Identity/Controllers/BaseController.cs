using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ShishByzh.Identity.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    internal Guid UserId => !User.Identity!.IsAuthenticated
        ? Guid.Empty
        : Guid.Parse(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
    
    internal string UserName => !User.Identity!.IsAuthenticated
        ? string.Empty
        : User.Claims.First(x => x.Type == ClaimTypes.Name).Value;
    
    internal string UserRoleName => !User.Identity!.IsAuthenticated
        ? string.Empty
        : User.Claims.First(x => x.Type == ClaimTypes.Role).Value;
}