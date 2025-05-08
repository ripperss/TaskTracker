using TaskTracker.Domain.Common;
using TaskTracker.Domain.TasksUser;
using TaskTracker.Domain.Users;

namespace TaskTracker.Domain.TaskComments;

public class TaskComment : Entity
{
    public Guid TaskId { get; private set; }
    public Guid UserId { get; private set; }
    public string Text { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Tasks Task { get; private set; } 
    public User User { get; private set; }

    protected TaskComment() { }

    public static TaskComment Create(
        Tasks task,
        User author,
        string text)
    {
        if (task == null)
            throw new Exception("Task is required");

        if (author == null)
            throw new Exception("Author is required");

        if (string.IsNullOrWhiteSpace(text))
            throw new Exception("Comment text cannot be empty");

        if (text.Length > 500)
            throw new Exception("Comment is too long (max 500 chars)");

        return new TaskComment
        {
            TaskId = task.Id,
            UserId = author.Id,
            Text = text,
            CreatedAt = DateTime.UtcNow,
            Task = task,
            User = author
        };
    }

    public void UpdateText(string newText, User editor)
    {
        if (editor.Id != UserId && editor.Role != Roles.Manager)
            throw new Exception("Only author can edit the comment");

        if (string.IsNullOrWhiteSpace(newText))
            throw new Exception("Text cannot be empty");

        Text = newText;
    }
}
