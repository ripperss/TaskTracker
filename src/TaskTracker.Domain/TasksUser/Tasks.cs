using System.Net.NetworkInformation;
using TaskTracker.Domain.Common;
using TaskTracker.Domain.TaskComments;
using TaskTracker.Domain.TaskParticipants;
using TaskTracker.Domain.TasksUser;
using TaskTracker.Domain.Tems;
using TaskTracker.Domain.Users;

namespace TaskTracker.Domain.TasksUser;

public class Tasks : Entity
{
    private readonly List<TaskComment> _comments = new();
    private readonly List<TaskParticipant> _participants = new();

    private Status _status;

    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; private set; } 
    public Guid? UserId { get; set; } 
    public Guid TeamId { get; set; } 
    public DateTime CreatedAt { get; set; }

    public IReadOnlyCollection<TaskComment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<TaskParticipant> Participants => _participants.AsReadOnly();

    public static Tasks Create(string title, string description, User creator)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new Exception("Title is required");

        return new Tasks
        {
            Title = title,
            Description = description,
            Status = Status.News,
            CreatedAt = DateTime.UtcNow,
            Id = Guid.NewGuid(),
        };
    }

    public void  AddComment(User user, string comment)
    {
        if(comment == null)
            throw new ArgumentNullException(nameof(comment));
        
        if(_comments.Count > 10)
            throw new CommentOverflowException();
        

        if(comment.Length <= 0 || comment == null) 
            throw new ArgumentOutOfRangeException(nameof(comment));

        _comments.Add(TaskComment.Create(this, user, comment));
    }

    public void AssignTo(User user, Team team)
    {
        if (!team.IsMember(user.Id))
            throw new Exception("User is not in the team");

        if (_status == Status.Completed)
            throw new Exception("Cannot assign completed tasks");

        _participants.Add(new TaskParticipant() { Id = user.Id, TaskId = this.Id, Task = this, User = user});
    }
}
