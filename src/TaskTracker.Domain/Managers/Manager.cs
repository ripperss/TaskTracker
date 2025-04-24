using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskTracker.Domain.Common;
using TaskTracker.Domain.Tems;
using TaskTracker.Domain.Users;

namespace TaskTracker.Domain.Managers;

public class Manager : Entity
{   
    public int UserId { get; set; }
    public User User { get; set; }

    public int GroupId { get; set; }
    public Team Team { get; set; }
}
