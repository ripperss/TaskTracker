using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;

namespace TaskTracker.Domain.Managers.Events;

public class ManagerCreatedEvent : IDomainEvent
{
    public int userId { get; set; }
    public int managerId { get; set; }
    public int teamId { get; set; }

    public ManagerCreatedEvent(int userId, int managerId, int teamId)
    {
        this.userId = userId;
        this.managerId = managerId;
        this.teamId = teamId;
    }
}
