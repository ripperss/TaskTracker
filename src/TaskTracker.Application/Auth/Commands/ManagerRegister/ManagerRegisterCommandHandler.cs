using MediatR;
using Microsoft.Extensions.Logging;
using TaskTracker.Application.Auth.Commands.Register;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Application.Common.Models;
using TaskTracker.Domain.Managers;
using TaskTracker.Domain.Tems;
using TaskTracker.Domain.Users;

namespace TaskTracker.Application.Auth.Commands.ManagerRegister;

public class ManagerRegisterCommandHandler : IRequestHandler<ManagerRegisterCommand, ManagerDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ManagerRegisterCommandHandler> _logger;
    private readonly ITeamRepositoty _teamRepositoty;
    private readonly IManagerRepository _managerRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserApplicationService _userApplicationService;

    public ManagerRegisterCommandHandler(
          IUnitOfWork unitOfWork
        , ILogger<ManagerRegisterCommandHandler> logger,
        ITeamRepositoty teamRepositoty,
        IManagerRepository managerRepository
        , IUserRepository userRepository
        , IUserApplicationService userApplicationService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _teamRepositoty = teamRepositoty;
        _managerRepository = managerRepository;
        _userRepository = userRepository;
        _userApplicationService = userApplicationService;
    }

    public async Task<ManagerDto> Handle(ManagerRegisterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("регистрация пользователя c ролью manager");

        var userRegisterCommand = new UserRegisterCommand(
            request.FirstName
            , request.LastName
            , request.Email
            , request.Password);

        var identityUser = await _userApplicationService.RegisterAsync(userRegisterCommand);

        var user = User.Create(identityUser.UserIdentityId, Roles.Manager);
        await _userRepository.CreateUserAsync(user);

        var team = Team.Create(request.TeamName, request.TeamPassword, user);
        await _teamRepositoty.AddTeamAsync(team);

        Manager manager = Manager.Create(user, team);
        await _managerRepository.AddManager(manager);

        await _unitOfWork.CommitChangesAsync();

        return new ManagerDto()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            UserIdentityId = user.IdentityUserId,
            Role = user.Role,
            TeamName = request.TeamName,
            TeamId = team.Id       
        };
    }
}
