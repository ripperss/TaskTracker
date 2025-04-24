using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;
using TaskTracker.Domain.TaskComments;
using TaskTracker.Domain.TaskParticipants;

namespace TaskTracker.Domain.Users;

public class User : Entity
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Roles Role { get; set; } 
    public DateTime CreatedAt { get; set; }
    public int TeamId { get; set; }

    public List<Task> Tasks { get; set; } = new List<Task>();

    public ICollection<TaskComment> TaskComments { get; set; } = new HashSet<TaskComment>();
    public ICollection<TaskParticipant> ParticipatedTasks { get; set; } = new List<TaskParticipant>();
}
