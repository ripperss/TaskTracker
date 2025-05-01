using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;

namespace TaskTracker.Domain.Users.Event;

public class UserLeftTeamEvent : IDomainEvent
{
    public Guid userId { get; set; }
    public Guid teamId { get; set; }

    public UserLeftTeamEvent(Guid userId, Guid teamId)
    {
        this.userId = userId;
        this.teamId = teamId;
    }
}
