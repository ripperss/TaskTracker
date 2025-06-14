﻿using TaskTracker.Domain.Common;
using TaskTracker.Domain.Managers.Events;
using TaskTracker.Domain.TasksUser;
using TaskTracker.Domain.Tems;
using TaskTracker.Domain.Tems.Events;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users;

namespace TaskTracker.Domain.Managers;

public class Manager : Entity
{   
    public Guid UserId { get; private set; }
    public Guid TeamId { get; private set; }

    public User User { get; private set; }
    public Team Team { get; private set; }

    private readonly List<Tasks> _tasks = new();
    public IReadOnlyCollection<Tasks> Tasks => _tasks.AsReadOnly();

    public static Manager Create(User user, Team team)
    {
        if (user.Role != Roles.Manager)
            throw new NoPermissionException("Only users with Manager role can be promoted");
        
        var manager = new Manager()
        {
            UserId = user.Id,
            TeamId = team.Id,
            Team = team,
            User = user
        };

        manager._domainEvents.Add(new ManagerCreatedEvent(user.Id, manager.Id, team.Id));

        return manager;
    }

    public void AddTask(Tasks task)
    {
        _tasks.Add(task);

        _domainEvents.Add(new AddTaskEvent()
        {
            TaskId = task.Id,
            TeamId = Team.Id,
        });
    }

    public void DeleteTask(Tasks task)
    {
        _tasks.Remove(task);

        _domainEvents.Add(new RemoveTaskEvent()
        {
            TaskId = task.Id,
            TeamId = Team.Id 
        });
    }
}
