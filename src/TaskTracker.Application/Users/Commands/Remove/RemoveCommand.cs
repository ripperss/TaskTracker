

using MediatR;

namespace TaskTracker.Application.Users.Commands.Remove;

public record RemoveCommand(string userIdentityId) : IRequest;

