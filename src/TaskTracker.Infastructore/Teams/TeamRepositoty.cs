using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Domain.Tems;
using TaskTracker.Infastructore.Common.Persistence;
using TaskTracker.Infastructore.Exceptions;

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

    public async Task<Team> GetTeamById(string teamId)
    {
        var team = await _context.Teams
            .Include(member => member.Members)         
            .FirstOrDefaultAsync(tm => tm.Id.ToString() == teamId)
                ?? throw new NotFoundTeamByIdException(teamId);

        return team;
    }

    public void UpdateTeam(Team team)
    {
        _context.Update(team);
    }
}
