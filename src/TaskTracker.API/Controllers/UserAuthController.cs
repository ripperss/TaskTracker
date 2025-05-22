using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.API.Models;
using TaskTracker.Application.Auth.Commands.Login;
using TaskTracker.Application.Auth.Commands.Register;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.API.Controllers;

[ApiController]
[Route("Auth")]

public class UserAuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserAuthController> _logger;

    public UserAuthController(IMediator mediator, ILogger<UserAuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("reg")]
    public async Task<IActionResult> RegisterAsync(UserRequestRegisterDto user)
    {
        var commnad = new UserRegisterCommand(user.FirstName
            , user.LastName
            , user.Email
            , user.Password);

        var registeUser = await _mediator.Send(commnad);

        return Created("registe User",registeUser);
    }

    [HttpPost("log")]
    public async Task<IActionResult> LoginAsync(UserLoginDto loginDto)
    {
        _logger.LogInformation(" авторизция с {loginDto}", loginDto);

        var commnad = new LoginCommand(loginDto.Email, loginDto.Password);

        var loginUser = await _mediator.Send(commnad);

        return Created("/api/login", loginUser);
    }
}
