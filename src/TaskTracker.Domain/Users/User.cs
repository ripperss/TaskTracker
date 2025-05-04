using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;
using TaskTracker.Domain.TaskComments;
using TaskTracker.Domain.TaskParticipants;
using TaskTracker.Domain.TasksUser;
using TaskTracker.Domain.Tems.Events;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users.Event;

namespace TaskTracker.Domain.Users;

public class User : Entity
{
    private string _name;
    private Roles _role;
    private Guid _teamId;
    private string _identityUserId;

    public string IdentityUserId => _identityUserId;
    public string Name => _name;
    public Roles Role => _role;
    public Guid TeamId => _teamId;
    public DateTime CreatedAt { get; private init; }

    public List<Tasks> Tasks { get; set; } = new List<Tasks>();

    public ICollection<TaskComment> TaskComments { get; set; } = new HashSet<TaskComment>();
    public ICollection<TaskParticipant> ParticipatedTasks { get; set; } = new List<TaskParticipant>();

    public static User Create(
         string name
        , string identityUserId
        , Roles roles = 0)
    {
        if (roles == Roles.Admin)
            throw new NoPermissionException("you cannot create a user with administrator rights");

        var user = new User()
        {
            CreatedAt = DateTime.Now,
            _name = name,
            _role = roles,
            Id = Guid.NewGuid(),
            _identityUserId = identityUserId
        };

        user._domainEvents.Add(new CreateUserEvent(user));

        return user;
    }

    public void LeaveTeam()
    {
        if (TeamId == Guid.Empty)
        {
            throw new Exception("the user is not a member of the team");
        }

        _domainEvents.Add(new UserLeftTeamEvent(Id, _teamId));

        _teamId = Guid.Empty;
    }

    public void AddTeam(Guid teamId, string teamPassword)
    {
        _teamId = teamId;

        _domainEvents.Add(new AddMembersOfTeamsEvent(this, teamId, teamPassword));
    }

    public void Delete()
    {
        if (Role == Roles.Admin)
        {
            throw new NoPermissionException("You cannot delete a user with an administrator role.");
        }

        _domainEvents.Add(new DeleteUserEvent(Id, TeamId, IdentityUserId));
    }
}