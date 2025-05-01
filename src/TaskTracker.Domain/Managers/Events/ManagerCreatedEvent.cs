using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;

namespace TaskTracker.Domain.Managers.Events;

public class ManagerCreatedEvent : IDomainEvent
{
    public Guid userId { get; set; }
    public Guid managerId { get; set; }
    public Guid teamId { get; set; }

    public ManagerCreatedEvent(Guid userId, Guid managerId, Guid teamId)
    {
        this.userId = userId;
        this.managerId = managerId;
        this.teamId = teamId;
    }
}
