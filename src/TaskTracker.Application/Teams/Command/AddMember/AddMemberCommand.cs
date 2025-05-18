
using MediatR;

namespace TaskTracker.Application.Teams.Command.AddMember;

public record AddMemberCommand(string teamId, string teamPassword, string userId) : IRequest
{
}
