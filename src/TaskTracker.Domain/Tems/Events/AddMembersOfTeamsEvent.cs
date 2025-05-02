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
    public Guid TeamId { get; set; }
    public string TeamPassword { get; set; }

    public AddMembersOfTeamsEvent(User user, Guid teamId, string teamPassword)
    {
        User = user;
        TeamId = teamId;
        TeamPassword = teamPassword;
    }

    public AddMembersOfTeamsEvent() { }
}
