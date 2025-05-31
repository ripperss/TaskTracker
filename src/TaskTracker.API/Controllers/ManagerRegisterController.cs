using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.API.Models;
using TaskTracker.Application.Auth.Commands.ManagerRegister;

namespace TaskTracker.API.Controllers;

[ApiController]
[Route("maangerReg")]
public class ManagerRegisterController : ControllerBase
{
    private readonly IMediator _mediator;

    public ManagerRegisterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Register(ManagerRegisterDto dto)
    {
        var managerCommnad = new ManagerRegisterCommand(
              dto.FirstName!
            , dto.LastName!
            , dto.Email!
            , dto.Password!
            , dto.TeamName!
            , dto.TeamPassword!
            , dto.ImageBase64);

        var result =  await _mediator.Send(managerCommnad);

        return Ok(result);
    }
}
