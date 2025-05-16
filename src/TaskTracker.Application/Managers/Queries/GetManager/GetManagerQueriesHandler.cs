using MediatR;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.Application.Managers.Queries.GetManager;

public class GetManagerQueriesHandler : IRequestHandler<GetManagerQueries, ManagerDto>
{
    private readonly IManagerRepository _managerRepository;
    private readonly IUserApplicationService _userApplicationService;

    public GetManagerQueriesHandler(
          IManagerRepository managerRepository
        , IUserApplicationService userApplicationService)
    {
        _managerRepository = managerRepository;
        _userApplicationService = userApplicationService;
    }

    public async Task<ManagerDto> Handle(GetManagerQueries request, CancellationToken cancellationToken)
    {
        var manager = await _managerRepository.GetManagerByIdentityId(request.identityId);

        var idnetityUser = await _userApplicationService.GetUserByIdAsync(request.identityId);

        var managerDto = new ManagerDto()
        {
            Email = idnetityUser.Email,
            FirstName = idnetityUser.FirstName,
            LastName = idnetityUser.LastName,
            Role = manager.User.Role,
            TeamId = manager.TeamId,
            TeamName = manager.Team.Name,
            UserIdentityId = manager.User.IdentityUserId 
        };

        return managerDto;
    }
}
