using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;
using TaskTracker.Domain.Managers.Events;
using TaskTracker.Domain.Tems;
using TaskTracker.Domain.Users;

namespace TaskTracker.Domain.Managers;

public class Manager : Entity
{   
    public int UserId { get; set; }
    public User User { get; set; }

    public int TeamId { get; set; }
    public Team Team { get; set; }

   public static Manager Create(string email
        , string passwordHasher
        , string name
        , string teamName
        , string teamPassword)
    {
        var user = User.Create(email , passwordHasher, name, Roles.Manager);

        var team = Team.Create(teamName, teamPassword, user);

        var manager = new Manager()
        {
            Id = user.Id,
            TeamId = team.Id,
            Team = team,
            User = user
        };

        manager._domainEvents.Add(new ManagerCreatedEvent(user.Id, manager.Id, team.Id));

        return manager;
    }
}
