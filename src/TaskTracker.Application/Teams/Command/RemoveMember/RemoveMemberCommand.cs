
using MediatR;

namespace TaskTracker.Application.Teams.Command.RemoveMember;

public record RemoveMemberCommand(string userId, string teamId) : IRequest
{

}

