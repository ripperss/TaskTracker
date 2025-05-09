
using Microsoft.EntityFrameworkCore;
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
        await _dbContext.DomainUsers.AddAsync(user);

        return user; 
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var user = await GetByIdAsync(id);

        user.Delete();

        _dbContext.DomainUsers.Remove(user);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        var users = await _dbContext.DomainUsers.ToListAsync();

        return users;
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        var user = await _dbContext.DomainUsers.FirstOrDefaultAsync(us => us.Id == id);

        if (user == null)
            throw new UserNotFoundException("the user was not found");

        return user;
    }

    public async Task<User> GetByIdentityIdAsync(string id)
    {
        var user = await _dbContext.DomainUsers.FirstOrDefaultAsync(us => us.IdentityUserId == id);

        if (user == null)
            throw new UserNotFoundException("the user was not found");

        return user;
    }
}
