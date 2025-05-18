

using TaskTracker.Application.Common.Models;
using TaskTracker.Domain.Tems;

namespace TaskTracker.Application.Common.Interfaces;

public interface ITeamRepositoty
{
    public Task AddTeamAsync(Team team);
    public Task<Team> GetTeamById(string teamId);
    public void UpdateTeam(Team team);
}
