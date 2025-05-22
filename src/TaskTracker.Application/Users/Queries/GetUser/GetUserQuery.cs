using MediatR;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.Application.Users.Queries.GetUser;

public record GetUserQuery(string ApplicatioUserId) : IRequest<UserDto>
{
}
