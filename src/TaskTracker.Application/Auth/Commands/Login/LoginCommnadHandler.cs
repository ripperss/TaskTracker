using MediatR;
using Microsoft.Extensions.Logging;
using TaskTracker.Application.Common.Interfaces;

namespace TaskTracker.Application.Auth.Commands.Login;

public class LoginCommnadHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserApplicationService _userApplicationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGeneration _jwtToken;
    private  readonly ILogger<LoginCommnadHandler> _logger;

    public LoginCommnadHandler(
        IUserRepository userRepository
        , IUserApplicationService userApplicationService
        , IUnitOfWork unitOfWork
        , IJwtTokenGeneration jwtToken
        , ILogger<LoginCommnadHandler> logger)
    {
        _userRepository = userRepository;
        _userApplicationService = userApplicationService;
        _unitOfWork = unitOfWork;
        _jwtToken = jwtToken;
        _logger = logger;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userIdentity = await _userApplicationService.GetUserByEmailAsync(request.Email);

        var domainUser = await _userRepository.GetByIdentityIdAsync(userIdentity.UserIdentityId);
        if (domainUser.Role == Domain.Users.Roles.Admin)
        {
            _logger.LogWarning("Админ пытался зарегаться через обычный Login");
            throw new InvalidOperationException("админ не может здесь залогиниться");
        }

        await _userApplicationService.EnsurePasswordIsCorrectAsync(request.Email, request.Password);

        _logger.LogInformation("генерация токена");
        var token = await _jwtToken.GenerationJwtToken(domainUser);

        await _unitOfWork.CommitChangesAsync();

        return token;
    }
}
