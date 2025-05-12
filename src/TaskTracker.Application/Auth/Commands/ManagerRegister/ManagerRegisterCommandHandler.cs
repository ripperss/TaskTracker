using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Common.Models;

namespace TaskTracker.Application.Auth.Commands.ManagerRegister;

public class ManagerRegisterCommandHandler : IRequestHandler<ManagerRegisterCommand, ManagerDto>
{
    
    
    public Task<ManagerDto> Handle(ManagerRegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
