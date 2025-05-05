using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Auth.Commands.Register;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.Application.Common.Interfaces;

public interface IUserApplicationService
{
    Task<UserResponseRegisterDto> RegisterAsync(UserRegisterCommand dto);
}
