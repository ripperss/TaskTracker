using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;
using TaskTracker.Domain.TasksUser;
using TaskTracker.Domain.Users;

namespace TaskTracker.Domain.TaskParticipants;

public class TaskParticipant : Entity
{
    public int TaskId { get; set; }
    public Tasks Task { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
}
