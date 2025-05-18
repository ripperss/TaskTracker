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

        var usersWithIdentity = await Task.WhenAll(team.Members.Select(async user =>
        {
            var identityUser = await _userApplicationService.GetUserByIdAsync(user.IdentityUserId);
            return new UserDto
            {
                UserIdentityId = user.IdentityUserId,
                Role = user.Role,
                TeamId = request.teamId,
                FirstName = identityUser.FirstName,
                LastName = identityUser.LastName,
                Email = identityUser.Email
            };
        }));

        return new TeamDto
        {
            TeamID = team.Id,
            Name = team.Name,
            AdminId = team.AdminId,
            CreatedAt = team.CreatedAt,
            Members = usersWithIdentity.ToList()
        };
    }
}

