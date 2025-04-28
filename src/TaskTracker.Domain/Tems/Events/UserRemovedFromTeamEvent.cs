using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;

namespace TaskTracker.Domain.Tems.Events;

public class UserRemovedFromTeamEvent : IDomainEvent
{
    public int teamId { get; set; }
    public int userId { get; set; }
    public int managerId { get; set; }

    public UserRemovedFromTeamEvent(int teamId, int userId, int managerId)
    {
        this.teamId = teamId;
        this.userId = userId;
        this.managerId = managerId;
    }
}
