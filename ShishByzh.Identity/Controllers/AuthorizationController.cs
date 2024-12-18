using Microsoft.AspNetCore.Mvc;
using ShishByzh.Application.Identity.Authorization.Queries.IsInRole;

namespace ShishByzh.Identity.Controllers;

public class AuthorizationController : BaseController
{
    [HttpGet("is-in-role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> IsInRole(Guid userId, string role)
    {
        var query = new Query(userId, role);
        
        return Ok(await Mediator.Send(query));
    }
    
    [HttpGet("is-in-policy")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> IsInPolicy(Guid userId, string policy)
    {
        var query = new Application.Identity.Authorization.Queries.IsInPolicy.Query(userId, policy);
        
        return Ok(await Mediator.Send(query));
    }
    
    [HttpGet("get-user-name")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<string?>> GetUserName(Guid userId)
    {
        var query = new Application.Identity.Authorization.Queries.GetUserName.Query(userId);

        return Ok(await Mediator.Send(query));
    }
}