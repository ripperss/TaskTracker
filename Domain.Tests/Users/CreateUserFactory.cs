using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TaskTracker.Domain.Users;

namespace Domain.Tests.Users;

public class CreateUserFactory
{
    public User CreateValidUser(string identityUserId)
    {
        var user = User.Create("fffff", identityUserId,Roles.User);

        return user;
    }

    public User CreateInvalidUser(string identityUserId)
    {
        var user = User.Create("fffff", identityUserId, Roles.Admin);

        return user;
    }

    public User CreateUserWithRoleManager(string identityUserId)
    {
        var user = User.Create("fffff", identityUserId, Roles.Manager);

        return user;
    }
}
