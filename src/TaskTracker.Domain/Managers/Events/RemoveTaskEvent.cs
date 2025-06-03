using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;

namespace TaskTracker.Domain.Managers.Events;

public class RemoveTaskEvent : IDomainEvent
{
    public Guid TeamId { get; set; }
    public Guid TaskId { get; set; }
}
