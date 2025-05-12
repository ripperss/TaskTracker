using TaskTracker.Domain.Common;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users.Event;

namespace TaskTracker.Domain.Users;

public class User : Entity
{
    public Roles Role { get; private set; }
    public Guid? TeamId { get; private set; }
    public string IdentityUserId { get; private set; }
    public DateTime CreatedAt { get; private init; }

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
        if (TeamId == null)
        {
            throw new Exception("the user is not a member of the team");
        }

        TeamId = null;
    }

    public void JoinTeam(Guid teamId)
    {
        if (TeamId != null)
            throw new InvalidOperationException("User is already in a team.");
        
        TeamId = teamId; 
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