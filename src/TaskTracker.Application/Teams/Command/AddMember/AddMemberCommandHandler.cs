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

    public Task Handle(AddMemberCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
