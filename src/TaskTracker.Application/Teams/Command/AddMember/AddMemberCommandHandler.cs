using MediatR;
using TaskTracker.Application.Common.Interfaces;

namespace TaskTracker.Application.Teams.Command.AddMember;

public class AddMemberCommandHandler : IRequestHandler<AddMemberCommand>
{
    private readonly ITeamRepositoty _teamRepositoty;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddMemberCommandHandler(
        ITeamRepositoty teamRepositoty
        , IUserRepository userRepository
        , IUnitOfWork unitOfWork)
    {
        _teamRepositoty = teamRepositoty;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {
        var team = await _teamRepositoty.GetTeamById(request.teamId);
        var user = await _userRepository.GetByIdentityIdAsync(request.userId);

        team.AddMember(user, request.teamPassword);

        await _unitOfWork.CommitChangesAsync();
    }
}
