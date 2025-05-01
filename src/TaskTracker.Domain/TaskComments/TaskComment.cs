using TaskTracker.Domain.Common;
using TaskTracker.Domain.TasksUser;
using TaskTracker.Domain.Users;

namespace TaskTracker.Domain.TaskComments;

public class TaskComment : Entity
{
    private Guid _taskId;
    private Guid _userId;
    private string _text;
    private DateTime _createdAt;

    public Guid TaskId => _taskId;
    public Guid UserId => _userId;
    public string Text => _text;
    public DateTime CreatedAt => _createdAt;

    public Tasks Task { get; private set; } 
    public User User { get; private set; }

    private TaskComment() { }

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
            _taskId = task.Id,
            _userId = author.Id,
            _text = text,
            _createdAt = DateTime.UtcNow,
            Task = task,
            User = author
        };
    }

    public void UpdateText(string newText, User editor)
    {
        if (editor.Id != _userId && editor.Role != Roles.Manager)
            throw new Exception("Only author can edit the comment");

        if (string.IsNullOrWhiteSpace(newText))
            throw new Exception("Text cannot be empty");

        _text = newText;
    }
}
