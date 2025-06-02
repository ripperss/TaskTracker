using MediatR;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.Application.Teams.Queries;

public class GetTeamQueriesHandler : IRequestHandler<GetTeamQueries, TeamDto>
{
    private readonly ITeamRepositoty _teamRepositoty;
    private readonly IUserApplicationService _userApplicationService;

    public GetTeamQueriesHandler(
          ITeamRepositoty teamRepositoty
        , IUserApplicationService userApplicationService)
    {
        _teamRepositoty = teamRepositoty;
        _userApplicationService = userApplicationService;
    }

    public async Task<TeamDto> Handle(GetTeamQueries request, CancellationToken cancellationToken)
    {
        var team = await _teamRepositoty.GetTeamById(request.teamId);

        var members = await _userApplicationService
            .GetIdentityUsersByIds(team.Members.Select(user => user.IdentityUserId));

        members.ForEach(user =>
        {
            var member = team.Members.First(m => m.IdentityUserId == user.UserIdentityId);
            
            user.Role = member.Role;
            user.TeamId = member.TeamId.ToString();

        });

        return new TeamDto
        {
            TeamID = team.Id,
            Name = team.Name,
            AdminId = team.AdminId,
            CreatedAt = team.CreatedAt,
            Members = members,
        };
    }
}


