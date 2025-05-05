using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.Auth.Commands.Register;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.API.Controllers;

[ApiController]
[Route("Auth")]

public class UserAuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserAuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("reg")]
    public async Task<IActionResult> Register(UserRequestRegisterDto user)
    {
        var commnad = new UserRegisterCommand(user.FirstName
            , user.LastName
            , user.Email
            , user.Password);

        var registeUser = await _mediator.Send(commnad);

        return Created("registe User",registeUser);

    }
}
