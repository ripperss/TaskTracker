

using MediatR;
using System.Runtime.InteropServices;
using TaskTracker.Application.Common.Interfaces;

namespace TaskTracker.Application.Users.Commands.Remove;

public class RemoveCommandHandler : IRequestHandler<RemoveCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserApplicationService _userApplicationService;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCommandHandler(
        IUserRepository userRepository
        , IUserApplicationService userApplicationService
        , IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userApplicationService = userApplicationService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdentityIdAsync(request.userIdentityId);

         var deleteUdentityUser = _userApplicationService.RemoveUserAsync(request.userIdentityId);
         var deleteUser = _userRepository.DeleteByIdAsync(user.Id);

        await Task.WhenAll(deleteUdentityUser, deleteUser);

        await _unitOfWork.CommitChangesAsync();
    }
}
