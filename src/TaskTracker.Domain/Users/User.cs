using TaskTracker.Domain.Common;
using TaskTracker.Domain.TasksUser;
using TaskTracker.Domain.Tems.Events;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users.Event;

namespace TaskTracker.Domain.Users;

public class User : Entity
{
    public Roles Role { get; private set; }
    public Guid? TeamId { get; private set; }
    public string IdentityUserId { get; private set; }
    public DateTime CreatedAt { get; private init; }

    public List<Tasks> Tasks { get; set; } = new List<Tasks>();

    protected User() { }

    public static User Create(
          string identityUserId
        , Roles roles = 0)
    {
        if (roles == Roles.Admin)
            throw new NoPermissionException("you cannot create a user with administrator rights");

        var user = new User()
        {
            CreatedAt = DateTime.Now,
            Role = roles,
            Id = Guid.NewGuid(),
            IdentityUserId = identityUserId
        };

        user._domainEvents.Add(new CreateUserEvent(user));

        return user;
    }

    public void LeaveTeam()
    {
        if (TeamId == Guid.Empty || TeamId == null)
        {
            throw new Exception("the user is not a member of the team");
        }

        _domainEvents.Add(new UserLeftTeamEvent(Id, TeamId));

        TeamId = Guid.Empty;
    }

    public void AddTeam(Guid teamId, string teamPassword)
    {
        TeamId = teamId;

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