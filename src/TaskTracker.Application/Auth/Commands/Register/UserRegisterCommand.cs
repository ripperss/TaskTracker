using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.Application.Auth.Commands.Register;

public record UserRegisterCommand(string Name, string Email, string Password) : IRequest<UserResponseRegisterDto>
{
}
