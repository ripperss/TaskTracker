using TaskTracker.Domain.Common;
using TaskTracker.Domain.Managers;
using TaskTracker.Domain.Tems.Events;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users;

namespace TaskTracker.Domain.Tems;

public class Team : Entity
{
    private string _name;
    private DateTime _createdAt;
    private readonly List<User> _members = new();
    private int _adminId { get; set; }
    private string _teamPassword { get; set; }

    public string Name => _name;
    public DateTime CreatedAt => _createdAt;
    public string TeamPassword => _teamPassword;    
    public int AdminId  => _adminId;
    public IReadOnlyCollection<User> Members => _members.AsReadOnly();

    private Team() { }

    public static Team Create(string name,string teamPassword , User admin)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new NullReferenceException("Team name is required");

        if (admin == null)
            throw new NullReferenceException("Admin is required");

        if (admin.Role != Roles.Manager)
            throw new NoPermissionException("Only users with the manager role can create teams.");

        var team = new Team
        {
            _name = name,
            _adminId = admin.Id,
            _createdAt = DateTime.UtcNow,
            _teamPassword = teamPassword
        };

        team.AddMember(admin, teamPassword);
        return team;

    }

    public void AddMember(User user, string teamPassword)
    {
        AddValidateData(user, teamPassword);

        _members.Add(user);

        _domainEvents.Add(new AddMembersOfTeamsEvent(user, user.Id));
    }

    public bool IsMember(int userId)
    {
        return _members.Any(m => m.Id == userId);
    }

    public void RemoveMember(User userToRemove, Manager initiator)
    {
        RemoveValidateData(initiator.Id, Id, userToRemove);

        _domainEvents.Add(new UserRemovedFromTeamEvent(Id, userToRemove.Id, initiator.Id));
    }

    public void ApplyUserLeft(User user)
    {
        if(!_members.Contains(user))
        {
            throw new UserNotFoundException("the user not found");
        }

        _members.Remove(user);
    }

    private void AddValidateData(User user, string teamPassword)
    {
        if (_members.Any(m => m.Id == user.Id))
            throw new UserAlreadyExists("User is already in the team");

        if (teamPassword == null)
            throw new NullReferenceException("the password cannot be Null");

        if (teamPassword != _teamPassword)
            throw new IncorrectPasswordExcepton("password incorect");   
    }

    private void RemoveValidateData(int initiorId, int teamId, User userToRemove)
    {
        if (teamId != Id)
            throw new NoPermissionException("No permission to remove this user");

        if (userToRemove.Id == _adminId)
            throw new NoPermissionException("Cannot remove team admin");

        if (!_members.Remove(userToRemove))
            throw new UserAlreadyExists("User not found in team");
    }
}
