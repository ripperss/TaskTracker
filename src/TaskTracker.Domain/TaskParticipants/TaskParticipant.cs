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
    public Guid  TaskId { get; private set; }
    public Tasks Task { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; }

    public DateTime JoinedAt { get;  private  init; } = DateTime.UtcNow;

    public static TaskParticipant Create(Tasks task, User user)
    {
        var TaskParticipant = new TaskParticipant()
        {
            Task = task,
            User = user,
            TaskId = task.Id,
            UserId = user.Id,
        };

        return TaskParticipant;
    }
}
