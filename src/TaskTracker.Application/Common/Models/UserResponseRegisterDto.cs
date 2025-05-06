using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Users;

namespace TaskTracker.Application.Common.Models;

public class UserResponseRegisterDto
{
    public string UserIdentityId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Roles Role { get; set; }
}
