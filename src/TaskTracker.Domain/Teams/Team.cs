using TaskTracker.Domain.Common;
using TaskTracker.Domain.Managers;
using TaskTracker.Domain.Tems.Events;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users;

namespace TaskTracker.Domain.Tems;

public class Team : Entity
{
    private readonly List<User> _members = new();

    public string Name {  get; private set; }
    public DateTime CreatedAt {  get; private set; }
    public string TeamPassword {  get; private set; } 
    public Guid AdminId {  get; private set; }
    public IReadOnlyCollection<User> Members => _members.AsReadOnly();

    private Team() { }

    public static Team Create(string name,string teamPassword , User admin)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Team name is required");

        if (string.IsNullOrWhiteSpace(teamPassword))
            throw new ArgumentException("Team password is required", nameof(teamPassword));

        if (admin == null)
            throw new ArgumentException("Admin is required");

        if (admin.Role != Roles.Manager)
            throw new NoPermissionException("Only users with the manager role can create teams.");

        var team = new Team
        {
            Name = name,
            AdminId = admin.Id,
            CreatedAt = DateTime.UtcNow,
            TeamPassword = teamPassword,
            Id = Guid.NewGuid()
        };

        team._members.Add(admin);

        return team;

    }

    public void AddMember(User user, string teamPassword)
    {
        AddValidateData(user, teamPassword);

        user.JoinTeam(Id);
        _members.Add(user);

        _domainEvents.Add(new AddMembersOfTeamsEvent(user, Id));
    }

    public bool IsMember(Guid userId)
    {
        return _members.Any(m => m.Id == userId);
    }

    public void RemoveMember(User userToRemove)
    {
        RemoveValidateData(Id, userToRemove);

        _members.Remove(userToRemove);
        userToRemove.LeaveTeam();

        _domainEvents.Add(new UserRemovedFromTeamEvent(Id, userToRemove.Id));
    }

    private void AddValidateData(User user, string teamPassword)
    {
        if (user.Role == Roles.Manager)
            throw new NoPermissionException("the manager cannot add himself to another team");

        if (_members.Any(m => m.Id == user.Id))
            throw new UserAlreadyExists("User is already in the team");

        if (teamPassword == null)
            throw new NullReferenceException("the password cannot be Null");

        if (teamPassword != TeamPassword)
            throw new IncorrectPasswordExcepton("password incorect");   
    }

    private void RemoveValidateData(Guid teamId, User userToRemove)
    {
        if (teamId != Id)
            throw new NoPermissionException("No permission to remove this user");

        if (userToRemove.Id == AdminId)
            throw new NoPermissionException("Cannot remove team admin");

        if (!_members.Contains(userToRemove))
            throw new UserAlreadyExists("User not found in team");
    }
}
