using MediatR;
using Microsoft.Extensions.Logging;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    private readonly IUserApplicationService _userApplicationService;
    private readonly ILogger<GetUsersQueryHandler> _logger;
    private readonly IUserRepository _userRepository;

    public GetUsersQueryHandler(
        IUserApplicationService userApplicationService
        , ILogger<GetUsersQueryHandler> logger,
        IUserRepository userRepository)
    {
        _userApplicationService = userApplicationService;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("try get users");

        var applicationUsers = await _userApplicationService.GetUsersAsync();
        var users = await _userRepository.GetAllAsync();

        var usersDto = (from appUser in applicationUsers
                        join user in users
                        on appUser.UserIdentityId equals user.IdentityUserId
                        select new UserDto
                        {
                            FirstName = appUser.FirstName,
                            LastName = appUser.LastName,
                            Email = appUser.Email,
                            UserIdentityId = appUser.UserIdentityId,
                            Role = user.Role,
                            TeamId = user.TeamId?.ToString(),
                            ImagePath = appUser.ImagePath
                        })
                        .ToList();

        return usersDto;
    }
}
