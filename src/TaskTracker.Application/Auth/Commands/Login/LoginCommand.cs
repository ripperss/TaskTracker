using MediatR;

namespace TaskTracker.Application.Auth.Commands.Login;

public record LoginCommand (string Email, string Password) : IRequest<string>;
