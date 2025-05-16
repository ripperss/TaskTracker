using MediatR;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.Application.Managers.Queries.GetManager;

public record GetManagerQueries(string identityId) : IRequest<ManagerDto>;
