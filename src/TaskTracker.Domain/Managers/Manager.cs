using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;
using TaskTracker.Domain.Managers.Events;
using TaskTracker.Domain.Tems;
using TaskTracker.Domain.Tems.Events;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users;

namespace TaskTracker.Domain.Managers;

public class Manager : Entity
{   
    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid TeamId { get; set; }
    public Team Team { get; set; }

   public static Manager Create(User user, Team team)
    {
        if (user.Role != Roles.Manager)
            throw new NoPermissionException("Only users with Manager role can be promoted");
        
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

    public void RemoveMemberOfTeams(User user)
    {
        if ((user.Role == Roles.Manager || user.TeamId != TeamId))
            throw new NoPermissionException("Not");

        user.LeaveTeam();

        _domainEvents.Add(new UserRemovedFromTeamEvent(TeamId, user.Id,UserId));
    }
    
}
