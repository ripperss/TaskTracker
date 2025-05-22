

using MediatR;
using TaskTracker.Application.Common.Interfaces;

namespace TaskTracker.Application.Teams.Command.RemoveMember;

public class RemoveMemberCommandHandler : IRequestHandler<RemoveMemberCommand>
{
    private readonly ITeamRepositoty _teamRepositoty;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveMemberCommandHandler(
        ITeamRepositoty teamRepositoty
        , IUserRepository userRepository
        , IUnitOfWork unitOfWork)
    {
        _teamRepositoty = teamRepositoty;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveMemberCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepositoty.GetTeamById(request.teamId);
        var user = await _userRepository.GetByIdentityIdAsync(request.userId);

        team.RemoveMember(user);

        await _unitOfWork.CommitChangesAsync();
    }
}
