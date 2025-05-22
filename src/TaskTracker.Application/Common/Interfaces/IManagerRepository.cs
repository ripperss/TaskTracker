

using TaskTracker.Domain.Managers;

namespace TaskTracker.Application.Common.Interfaces;

public interface IManagerRepository
{
    public Task AddManager(Manager manager);
    public Task<Manager> GetManagerByIdentityId(string identityId);
    public Task<Manager> GetManagerById(Guid managerId);
    public void UpdateManager(Manager manager);
}
