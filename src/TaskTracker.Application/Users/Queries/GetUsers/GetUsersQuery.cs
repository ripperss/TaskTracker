
using MediatR;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.Application.Users.Queries.GetUsers;

public class GetUsersQuery : IRequest<List<UserDto>>
{
}
