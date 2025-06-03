using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;

namespace TaskTracker.Domain.TasksUser.Events;

public class ChangeStatusTaskEvent : IDomainEvent
{
    public Guid TaskId { get; set; }
    public string Status { get; set; }
    public Guid UserId { get; set; }
}
