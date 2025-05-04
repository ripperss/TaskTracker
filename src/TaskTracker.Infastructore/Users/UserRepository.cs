using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Data.Entity;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users;
using TaskTracker.Infastructore.Common.Persistence;

namespace TaskTracker.Infastructore.Users;

public class UserRepository : IUserRepository
{
    private readonly TaskTrackerDbContext _dbContext;

    public UserRepository(TaskTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        var Dbuser = _dbContext.Users.FirstOrDefaultAsync(us => us.Name == user.Name);
        if (Dbuser != null)
            throw new UserAlreadyExists(user.Name);

        await _dbContext.Users.AddAsync(user);

        return user; 
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var user = await GetByIdAsync(id);

        user.Delete();

        _dbContext.Users.Remove(user);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _dbContext.Users.ToListAsync();

        return users;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(us => us.Id == id);

        if (user == null)
            throw new UserNotFoundException("the user was not found");

        return user;
    }

    public async Task<User> GetByIdentityIdAsync(string id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(us => us.IdentityUserId == id);

        if (user == null)
            throw new UserAlreadyExists("the user was not found");

        return user;
    }
}
