using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Domain.Managers;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Infastructore.Common.Persistence;

namespace TaskTracker.Infastructore.Managers;

public class ManagerRepository : IManagerRepository
{
    private readonly TaskTrackerDbContext _dbContext;

    public ManagerRepository(TaskTrackerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddManager(Manager manager)
    {
        await _dbContext.AddAsync(manager);
    }

    public async Task<Manager> GetManagerById(Guid managerId)
    {
        var manager = await _dbContext
            .Managers
            .FirstOrDefaultAsync(mngr => mngr.Id == managerId)
                ?? throw new UserNotFoundException("manager notFound");

        return manager;
    }

    public async Task<Manager> GetManagerByIdentityId(string identityId)
    {
        var manager = await _dbContext
            .Managers
            .Include(t => t.Team)
            .Include(u => u.User)
            .FirstOrDefaultAsync(mngr => mngr.User.IdentityUserId == identityId)
                ?? throw new UserNotFoundException("manager notFound");

        return manager;    
    }

    public void UpdateManager(Manager manager)
    {
        _dbContext.Update(manager);
    }
}