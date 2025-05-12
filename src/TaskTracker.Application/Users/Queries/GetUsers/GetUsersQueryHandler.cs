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

        var users = await _userApplicationService.GetUsersAsync();

        foreach (var userDto in users)
        {
            var user = await _userRepository.GetByIdentityIdAsync(userDto.UserIdentityId);
            userDto.Role = user.Role;
            userDto.TeamId = user.TeamId.ToString();
        }

        return users;
    }
}
