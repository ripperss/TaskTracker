using MediatR;
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

    public GetUserQueryHandler(
        IUserApplicationService userApplicationService)
    {
        _userApplicationService = userApplicationService;
    }

    public Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = _userApplicationService.GetUserByIdAsync(request.ApplicatioUserId);

        return user;
    }
}
