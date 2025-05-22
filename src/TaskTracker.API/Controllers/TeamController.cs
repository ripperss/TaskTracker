using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.API.Models;
using TaskTracker.API.Services.UserServices;
using TaskTracker.Application.Teams.Command.AddMember;
using TaskTracker.Application.Teams.Command.RemoveMember;
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

    [Authorize]
    [HttpPost("add_member")]
    public async Task<IActionResult> AddMemberAsync(AddMemberDto dto)
    {
        var userId = _jwtValidatorService.GetUserIdentityId();
        var addMemberCommand = new AddMemberCommand(dto.TeamId, dto.TeamPassword, userId);

        await _mediator.Send(addMemberCommand);

        return Created("Вы успешно зарегались в команде", dto.TeamId);    
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> RemoveMemberAsync()
    {
        var userId = _jwtValidatorService.GetUserIdentityId();
        var teamId = _jwtValidatorService.GetTeamId();

        var commnad = new RemoveMemberCommand(userId, teamId);

        await _mediator.Send(commnad);

        return NotFound();
    }
}
