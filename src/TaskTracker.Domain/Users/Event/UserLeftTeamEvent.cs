using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;

namespace TaskTracker.Domain.Users.Event;

public class UserLeftTeamEvent : IDomainEvent
{
    public int userId { get; set; }
    public int teamId { get; set; }

    public UserLeftTeamEvent(int userId, int teamId)
    {
        this.userId = userId;
        this.teamId = teamId;
    }
}
