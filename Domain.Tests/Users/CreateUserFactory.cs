using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Users;

namespace Domain.Tests.Users;

public class CreateUserFactory
{
    public User CreateValidUser()
    {
        var user = User.Create("fffff","ff","f",Roles.User);

        return user;
    }

    public User CreateInvalidUser()
    {
        var user = User.Create("fffff", "ff", "f", Roles.Admin);

        return user;
    }

    public User CreateUserWithRoleManager()
    {
        var user = User.Create("fffff", "ff", "f", Roles.Manager);

        return user;
    }
}
