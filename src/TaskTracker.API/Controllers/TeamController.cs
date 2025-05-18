using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.API.Services.UserServices;
using TaskTracker.Application.Teams.Queries;

namespace TaskTracker.API.Controllers;

[ApiController]
[Route("Team")]
public class TeamController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IJwtValidatorService _jwtValidatorService;

    public TeamController(IMediator mediator, IJwtValidatorService jwtValidatorService)
    {
        _mediator = mediator;
        _jwtValidatorService = jwtValidatorService;
    }

    [HttpGet("{teamId}")]
    public async Task<IActionResult> GetTeamAsync(string teamId)
    {
        var teamQueries = new GetTeamQueries(teamId);

        var team = await _mediator.Send(teamQueries);

        return Ok(team);
    }

    [HttpGet("MyTeam")]
    public async Task<IActionResult> GetMyTeam()
    {
        var teamId = _jwtValidatorService.GetTeamId();

        var teamQueries = new GetTeamQueries(teamId);

        var team = await _mediator.Send(teamQueries);

        return Ok(team);
    }

}
