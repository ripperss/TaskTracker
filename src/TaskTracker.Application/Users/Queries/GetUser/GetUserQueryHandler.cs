using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IUserApplicationService _userApplicationService;
    private readonly ILogger<GetUserQueryHandler> _logger;
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(
        IUserApplicationService userApplicationService
        , ILogger<GetUserQueryHandler> logger,
        IUserRepository userRepository)
    {
        _userApplicationService = userApplicationService;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Пытаюсь получить данные из Identity  ");
        var userDto = await _userApplicationService.GetUserByIdAsync(request.ApplicatioUserId);

         var user = await _userRepository.GetByIdentityIdAsync(userDto.UserIdentityId);

        userDto.Role = user.Role;
        userDto.TeamId = user.TeamId.ToString();

        _logger.LogInformation("получил  данные из Identity  ");
        _logger.LogInformation($"{userDto.FirstName}");
        return userDto;
    }
}
