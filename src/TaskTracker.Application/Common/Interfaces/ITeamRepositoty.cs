

using TaskTracker.Domain.Tems;

namespace TaskTracker.Application.Common.Interfaces;

public interface ITeamRepositoty
{
    public Task AddTeamAsync(Team team);
}
