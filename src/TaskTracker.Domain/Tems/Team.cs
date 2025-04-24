using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;
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
            throw new Exception("Team name is required");

        if (admin == null)
            throw new Exception("Admin is required");

        var team = new Team
        {
            _name = name,
            _adminId = admin.Id,
            _createdAt = DateTime.UtcNow,
            _teamPassword = teamPassword
        };

        team.AddMember(admin);
        return team;
    }

    public void AddMember(User user)
    {
        if (_members.Any(m => m.Id == user.Id))
            throw new Exception("User is already in the team");

        _members.Add(user);
    }

    public bool IsMember(int userId)
    {
        return _members.Any(m => m.Id == userId);
    }
}
