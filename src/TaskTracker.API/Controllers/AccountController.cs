using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskTracker.Application.Users.Commands.Remove;
using TaskTracker.Application.Users.Queries.GetUser;
using TaskTracker.Application.Users.Queries.GetUsers;

namespace TaskTracker.API.Controllers;

[Route("account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AccountController> _logger;    

    public AccountController(
          IMediator mediator
        , ILogger<AccountController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("GetMy")]
    [Authorize]
    public async Task<IActionResult> GetMyAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var getUserQuery = new GetUserQuery(userId!);

        var user = await _mediator.Send(getUserQuery);

        return Ok(user);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserAsync(string userId)
    {
        var getUserQuery = new GetUserQuery(userId);

        var user = await _mediator.Send(getUserQuery);

        return Ok(user);
    }

    [HttpGet("accounts")]
    public async Task<IActionResult> GetAllUserAsync()
    {
        var getUsersQuery = new GetUsersQuery();

        var users = await _mediator.Send(getUsersQuery);

        return Ok(users);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveUserAsync(string userId)
    {
        var command = new RemoveCommand(userId);

        await _mediator.Send(command);
        
        return NoContent();
    }
}
