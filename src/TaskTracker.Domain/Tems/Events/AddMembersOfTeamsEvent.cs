using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;
using TaskTracker.Domain.Users;

namespace TaskTracker.Domain.Tems.Events;

public class AddMembersOfTeamsEvent : IDomainEvent
{
    public User User { get; set; }
    public int UserId { get; set; }

    public AddMembersOfTeamsEvent(User user, int userId)
    {
        User = user;
        UserId = userId;
    }

    public AddMembersOfTeamsEvent() { }
}
