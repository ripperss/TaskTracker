using TaskTracker.Domain.Common;
using TaskTracker.Domain.Managers;
using TaskTracker.Domain.Managers.Events;
using TaskTracker.Domain.TaskComments;
using TaskTracker.Domain.TaskParticipants;
using TaskTracker.Domain.TasksUser.Events;
using TaskTracker.Domain.Tems;
using TaskTracker.Domain.Users;

namespace TaskTracker.Domain.TasksUser;

public class Tasks : Entity
{
    private readonly List<TaskComment> _comments = new();
    private readonly List<TaskParticipant> _participants = new();

    public string Title { get; private set; }
    public string Description { get; private set; }
    public Status Status { get; private set; } 
    public Guid? ManagerId { get; private set; } 
    public Guid TeamId { get; private set; } 
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<TaskComment> Comments => _comments.AsReadOnly();
    public IReadOnlyCollection<TaskParticipant> Participants => _participants.AsReadOnly();

    public static Tasks Create(string title, string description, Manager creator)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new Exception("Title is required");

         var task = new Tasks
        {
            Title = title,
            Description = description,
            Status = Status.News,
            CreatedAt = DateTime.UtcNow,
            Id = Guid.NewGuid(),
            ManagerId = creator.Id,
        };

        creator.AddTask(task);

        return task;
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

        if (Status == Status.Completed)
            throw new Exception("Cannot assign completed tasks");

        _participants.Add(TaskParticipant.Create(this, user));
    }

    public void RemoveTask(Manager manager)
    {
        if (ManagerId != manager.Id)
            throw new Exception("permission Denied");

        manager.DeleteTask(this);
    }

    public void ChangeStatusTask(Status status, User user)
    {
        PermissionUser(user);

        Status = status;

        _domainEvents.Add(new ChangeStatusTaskEvent()
        {
            TaskId = Id,
            Status = status.ToString(),
            UserId = user.Id,
        });
    }

    private void PermissionUser(User user)
    {
        if(user.Id != ManagerId)
            throw new Exception("permission Denied");

        var participant = Participants.FirstOrDefault(participant => participant.UserId == user.Id)
            ?? throw new Exception("permission Denied"); ;
    }
}
