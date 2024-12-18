using Microsoft.AspNetCore.Mvc;
using ShishByzh.Application.Identity.Authentication.Commands.Registration;
using ShishByzh.Application.Identity.Authentication.Queries.SignIn;

namespace ShishByzh.Identity.Controllers;

public class AuthenticationController : BaseController
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> Register(Command command)
    {
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
    
    [HttpPost("sign-in")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<LoginResponse>> 
        SignIn(Query query)
    {
        var result = await Mediator.Send(query);
        
        return result.IsSuccessful ?
            Ok(result) :
            NotFound(result.Errors);
    }
    
    [HttpPost("sign-out")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public new async Task<ActionResult> SignOut()
    {
        var query = new Application.Identity.Authentication.Queries.SignOut.Query();
        await Mediator.Send(query);
        
        return Ok();
    }

    [HttpGet("validate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ValidateToken(string token)
    {
        var query = new Application.Identity.Authentication.Queries.CheckToken.Query(token);

        var result = await Mediator.Send(query);
        
        return result.Succeeded ?
            Ok(result) :
            NotFound(result.Errors);
    }
}