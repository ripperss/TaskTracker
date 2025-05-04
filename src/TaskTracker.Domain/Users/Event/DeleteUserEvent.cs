using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;

namespace TaskTracker.Domain.Users.Event;

public class DeleteUserEvent : IDomainEvent
{
    public string IdentityId { get; set; }
    public Guid UserId { get; set; }
    public Guid TeamId { get; set; }

    public DeleteUserEvent(Guid userId, Guid teamId, string identityId)
    {
        IdentityId = identityId;
        UserId = userId;
        TeamId = teamId;
    }
}
