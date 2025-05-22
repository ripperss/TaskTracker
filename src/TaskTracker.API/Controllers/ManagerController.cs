using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.API.Services.UserServices;
using TaskTracker.Application.Managers.Queries.GetManager;

namespace TaskTracker.API.Controllers;

[ApiController]
[Route("manager")]
public class ManagerController : ControllerBase
{
    private readonly IJwtValidatorService _jwtValidatorService;
    private readonly IMediator _mediator;

    public ManagerController(
        IJwtValidatorService jwtValidatorService
        , IMediator mediator)
    {
        _jwtValidatorService = jwtValidatorService;
        _mediator = mediator;
    }

    [HttpGet("{identityId}")]
    public async Task<IActionResult> GetManager(string identityId) 
    {
        var command = new GetManagerQueries(identityId);    

        var manager = await _mediator.Send(command);

        return Ok(manager);
    }
}
