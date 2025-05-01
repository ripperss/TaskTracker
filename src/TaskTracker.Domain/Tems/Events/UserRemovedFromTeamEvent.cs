using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;

namespace TaskTracker.Domain.Tems.Events;

public class UserRemovedFromTeamEvent : IDomainEvent
{
    public Guid teamId { get; set; }
    public Guid userId { get; set; }
    public Guid managerId { get; set; }

    public UserRemovedFromTeamEvent(Guid teamId, Guid userId, Guid managerId)
    {
        this.teamId = teamId;
        this.userId = userId;
        this.managerId = managerId;
    }
}
