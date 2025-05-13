

using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Domain.Tems;
using TaskTracker.Infastructore.Common.Persistence;

namespace TaskTracker.Infastructore.Teams;

public class TeamRepositoty : ITeamRepositoty
{
    private readonly TaskTrackerDbContext _context;

    public TeamRepositoty(TaskTrackerDbContext context)
    {
        _context = context;
    }

    public async Task AddTeamAsync(Team team)
    {
        await _context.Teams.AddAsync(team);
    }
}
