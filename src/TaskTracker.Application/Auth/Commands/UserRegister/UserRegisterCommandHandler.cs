using MediatR;
using Microsoft.Extensions.Logging;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Application.Common.Models;
using TaskTracker.Domain.Users;

namespace TaskTracker.Application.Auth.Commands.Register;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, UserDto>
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
    
    public async Task<UserDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("регистрация пользователя");

        var imagePath = await _imageService.AploadImage(request.ImageBase64);

        var userDto = new UserRegisterDto()
        {
            ImagePath = imagePath,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password, 
        };

        var identityUser = await _userApplicationService.RegisterAsync(userDto);
        identityUser.Role = Roles.User;
        identityUser.ImagePath = imagePath;

        var user = User.Create(identityUser.UserIdentityId, Roles.User);
        await _userRepository.CreateUserAsync(user);

        await _unitOfWork.CommitChangesAsync();
        return identityUser;
    }
}
