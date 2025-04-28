using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;

namespace TaskTracker.Domain.Users.Event;

public class CreateUserEvent : IDomainEvent
{
    public User User { get; set; }

    public CreateUserEvent(User user) 
    { 
        User = user;
    }

}
