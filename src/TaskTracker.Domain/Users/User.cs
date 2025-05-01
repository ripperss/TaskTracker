using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;
using TaskTracker.Domain.TaskComments;
using TaskTracker.Domain.TaskParticipants;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users.Event;

namespace TaskTracker.Domain.Users;

public class User : Entity
{
    private string _name;
    private string _email;
    private string _passwordHash;
    private Roles _role;
    private int _teamId;

    public string Name => _name;
    public string Email => _email;
    public Roles Role => _role;
    public int TeamId => _teamId;
    public string PasswordHash => _passwordHash;
    public DateTime CreatedAt { get; private init; }

    public List<Task> Tasks { get; set; } = new List<Task>();

    public ICollection<TaskComment> TaskComments { get; set; } = new HashSet<TaskComment>();
    public ICollection<TaskParticipant> ParticipatedTasks { get; set; } = new List<TaskParticipant>();

    public static User Create(
        string email
        , string passwordHasher
        , string name
        , Roles roles = 0)
    {
        if (roles == Roles.Admin)
            throw new NoPermissionException("you cannot create a user with administrator rights");
        
        var user = new User()
        {
            CreatedAt = DateTime.Now,
            _email = email,
            _passwordHash = passwordHasher,
            _name = name,
            _role = roles      
        };

        user._domainEvents.Add(new CreateUserEvent(user));

        return user;
    } 
    
    public void LeaveTem()
    {
        if(TeamId == 0)
        {
            throw new Exception("the user is not a member of the team");
        }
        
        _domainEvents.Add(new UserLeftTeamEvent(Id, _teamId));

        _teamId = 0;
    }

    public void AddTeam(int teamId)
    {
        _teamId = teamId;
    }
}
