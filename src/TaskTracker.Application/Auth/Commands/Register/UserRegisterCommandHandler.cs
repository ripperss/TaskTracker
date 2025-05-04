using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.Application.Auth.Commands.Register;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, UserResponseRegisterDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGeneration _jwtTokenGeneration;

    public UserRegisterCommandHandler(IUserRepository userRepository, IJwtTokenGeneration jwtTokenGeneration)
    {
        _userRepository = userRepository;
        _jwtTokenGeneration = jwtTokenGeneration;
    }
    
    public Task<UserResponseRegisterDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
