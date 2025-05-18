

using MediatR;
using TaskTracker.Application.Common.Models;
using TaskTracker.Domain.Users;

namespace TaskTracker.Application.Teams.Queries;

public record GetTeamQueries(string teamId) : IRequest<TeamDto>
{
}
