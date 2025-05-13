

using TaskTracker.Domain.Managers;

namespace TaskTracker.Application.Common.Interfaces;

public interface IManagerRepository
{
    public Task AddManager(Manager manager);
}
