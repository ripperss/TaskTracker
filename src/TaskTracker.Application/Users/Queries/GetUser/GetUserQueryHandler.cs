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

    public GetUserQueryHandler(
        IUserApplicationService userApplicationService
        , ILogger<GetUserQueryHandler> logger)
    {
        _userApplicationService = userApplicationService;
        _logger = logger;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Пытаюсь получить данные из Identity  ");
        var user = await _userApplicationService.GetUserByIdAsync(request.ApplicatioUserId);

        _logger.LogInformation("получил  данные из Identity  ");
        _logger.LogInformation($"{user.FirstName}");
        return user;
    }
}
