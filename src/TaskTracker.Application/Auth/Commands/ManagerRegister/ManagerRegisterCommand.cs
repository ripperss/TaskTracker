using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.Application.Auth.Commands.ManagerRegister;

public record ManagerRegisterCommand(
    string FirstName
    , string LastName
    , string Email
    , string Password
    , string TeamName
    , string TeamPassword
    , string ImageBase64
    ) : IRequest<ManagerDto>
{
}
