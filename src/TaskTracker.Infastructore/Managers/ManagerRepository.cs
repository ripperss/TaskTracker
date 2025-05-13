using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Domain.Managers;
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
}
