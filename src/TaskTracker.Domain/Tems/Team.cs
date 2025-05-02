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
    private Guid _adminId { get; set; }
    private string _teamPassword { get; set; }

    public string Name => _name;
    public DateTime CreatedAt => _createdAt;
    public string TeamPassword => _teamPassword;    
    public Guid AdminId  => _adminId;
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
            _teamPassword = teamPassword,
            Id = Guid.NewGuid()
        };

        team._members.Add(admin);

        return team;

    }

    public void AddMember(User user, string teamPassword)
    {
        if(_members.Contains(user))
        {
            return;
        }

        AddValidateData(user, teamPassword);

        _members.Add(user);
    }

    public bool IsMember(Guid userId)
    {
        return _members.Any(m => m.Id == userId);
    }

    public void RemoveMember(User userToRemove)
    {
        RemoveValidateData(Id, userToRemove);

        _members.Remove(userToRemove);
    }

    private void AddValidateData(User user, string teamPassword)
    {
        if (user.Role == Roles.Manager)
            throw new NoPermissionException("the manager cannot add himself to another team");

        if (_members.Any(m => m.Id == user.Id))
            throw new UserAlreadyExists("User is already in the team");

        if (teamPassword == null)
            throw new NullReferenceException("the password cannot be Null");

        if (teamPassword != _teamPassword)
            throw new IncorrectPasswordExcepton("password incorect");   
    }

    private void RemoveValidateData(Guid teamId, User userToRemove)
    {
        if (teamId != Id)
            throw new NoPermissionException("No permission to remove this user");

        if (userToRemove.Id == _adminId)
            throw new NoPermissionException("Cannot remove team admin");

        if (!_members.Contains(userToRemove))
            throw new UserAlreadyExists("User not found in team");
    }
}
