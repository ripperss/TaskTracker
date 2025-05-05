using Microsoft.AspNetCore.Identity;
using TaskTracker.Application.Auth.Commands.Register;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Application.Common.Models;
using TaskTracker.Domain.Tems.Exceptions;

namespace TaskTracker.Infastructore.Identity;

public class UserApplicationService : IUserApplicationService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserApplicationService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<UserResponseRegisterDto> RegisterAsync(UserRegisterCommand dto)
    {
        var findUserByEmail = _userManager.FindByEmailAsync(dto.Email);
        var findUserByName = _userManager.FindByNameAsync(dto.FirstName);

        if (findUserByEmail != null || findUserByName != null)
            throw new UserAlreadyExists("пользователь с данным Email или именем уже есть ");
        
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Registration failed: {errors}");
        }

        return new UserResponseRegisterDto
        {
            UserIdentityId = user.Id.ToString(),
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    
}
