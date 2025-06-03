using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;

namespace TaskTracker.Domain.Managers.Events;

public class AddTaskEvent : IDomainEvent
{
    public Guid TaskId { get; set; }
    public Guid TeamId { get; set; }
}
