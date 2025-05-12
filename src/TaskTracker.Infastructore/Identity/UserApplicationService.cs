using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Auth.Commands.Register;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Application.Common.Models;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Infastructore.Exceptions;

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
        var findUserByEmail = await _userManager.FindByEmailAsync(dto.Email);
        var findUserByName = await _userManager.FindByNameAsync(dto.FirstName);

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

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(email)
            ?? throw new NotFoundUserBtEmailException("the user with this email was not found");

        var userDto = new UserDto
        {
            Email = email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserIdentityId = user.Id.ToString(),

        };

        return userDto;
    }

    public async Task EnsurePasswordIsCorrectAsync(string email, string inputPassword )
    {
        ApplicationUser user = await _userManager.FindByEmailAsync(email)
            ?? throw new NotFoundUserBtEmailException("the user with this email was not found");

        var result = await  _userManager.CheckPasswordAsync(user, inputPassword);

        if (result == false)
        {
            throw new Exception("пароль не верен");
        }
    }

    public async Task<UserDto> GetUserByIdAsync(string userId)
    {
        var user = await GetIdentityUser(userId);

        var userDto = new UserDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserIdentityId= user.Id.ToString(),
        };

        return userDto;
    } 

    public async Task RemoveUserAsync(string idnetityUserId)
    {
        var identityUser = await GetIdentityUser(idnetityUserId);

        var deleteAction = await _userManager.DeleteAsync(identityUser);
        if (!deleteAction.Succeeded)
            throw new Exception("Не удалось удалить");
    }

    public async Task<List<UserDto>> GetUsersAsync()
    {
        var identityUsers = await _userManager.Users.ToListAsync(); 

        var users = identityUsers.Select(identityUser => new UserDto
        {
            Email = identityUser.Email,
            FirstName = identityUser.FirstName,
            LastName = identityUser.LastName,
            UserIdentityId = identityUser.Id.ToString(),
        }).ToList();

        return users;
    }

    private async Task<ApplicationUser> GetIdentityUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new Exception();

        return user;    
    }
}
