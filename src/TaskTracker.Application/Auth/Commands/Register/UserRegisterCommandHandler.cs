using MediatR;
using Microsoft.Extensions.Logging;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Application.Common.Models;
using TaskTracker.Domain.Users;

namespace TaskTracker.Application.Auth.Commands.Register;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, UserResponseRegisterDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGeneration _jwtTokenGeneration;
    private readonly IUserApplicationService _userApplicationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UserRegisterCommandHandler> _logger;

    public UserRegisterCommandHandler(
        IUserRepository userRepository
        , IJwtTokenGeneration jwtTokenGeneration
        , IUserApplicationService userApplicationService
        , IUnitOfWork unitOfWork
        , ILogger<UserRegisterCommandHandler> logger)
    {
        _userRepository = userRepository;
        _jwtTokenGeneration = jwtTokenGeneration;
        _userApplicationService = userApplicationService;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    
    public async Task<UserResponseRegisterDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("регистрация пользователя");
        var identityUser = await _userApplicationService.RegisterAsync(request);
        identityUser.Role = Roles.User;

        var user = User.Create(request.FirstName, identityUser.UserIdentityId, Roles.User);
        await _userRepository.CreateUserAsync(user);

        _logger.LogInformation("генирация токена пользователя");
        string token = await _jwtTokenGeneration.GenerationJwtToken(user);
        identityUser.Token = token;

        await _unitOfWork.CommitChangesAsync();
        return identityUser;
    }
}
