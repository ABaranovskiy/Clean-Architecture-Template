using Microsoft.AspNetCore.Mvc;

namespace ShishByzh.Server.Controllers;

public class UserController : BaseController
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Application.Users.Queries.GetUsers.Vm>> GetUsers()
    {
        var query = new Application.Users.Queries.GetUsers.Query();
     
        return Ok(await Mediator.Send(query));
    }
    
    [HttpGet("{userId:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Application.Users.Queries.GetUser.UserDto>> GetUser(Guid userId)
    {
        var query = new Application.Users.Queries.GetUser.Query(userId);
        
        return Ok(await Mediator.Send(query));
    }
    
    [HttpDelete("{userId:Guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> DeleteUser(Guid userId)
    {
        var command = new Application.Users.Commands.DeleteUser.Command(userId);
        
        return Ok(await Mediator.Send(command));
    }
}