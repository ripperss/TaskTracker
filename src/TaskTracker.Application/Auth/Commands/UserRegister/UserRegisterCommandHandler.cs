using MediatR;
using Microsoft.Extensions.Logging;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Application.Common.Models;
using TaskTracker.Domain.Users;

namespace TaskTracker.Application.Auth.Commands.Register;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, UserResponseRegisterDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserApplicationService _userApplicationService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageService _imageService;
    private readonly ILogger<UserRegisterCommandHandler> _logger;

    public UserRegisterCommandHandler(
        IUserRepository userRepository
        , IUserApplicationService userApplicationService
        , IUnitOfWork unitOfWork
        , ILogger<UserRegisterCommandHandler> logger
        , IImageService imageService)
    {
        _userRepository = userRepository;
        _userApplicationService = userApplicationService;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _imageService = imageService;
    }
    
    public async Task<UserResponseRegisterDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("регистрация пользователя");
        var identityUser = await _userApplicationService.RegisterAsync(request);
        identityUser.Role = Roles.User;

        var imagePath = await _imageService.AploadImage(request.ImageBase64);
        identityUser.ImagePath = imagePath;

        var user = User.Create(identityUser.UserIdentityId, Roles.User);
        await _userRepository.CreateUserAsync(user);

        await _unitOfWork.CommitChangesAsync();
        return identityUser;
    }
}
