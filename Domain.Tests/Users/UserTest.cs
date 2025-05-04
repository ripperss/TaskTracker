using FluentAssertions;
using TaskTracker.Domain.Common;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users;
using TaskTracker.Domain.Users.Event;

namespace Domain.Tests.Users;

sealed public class UserTest
{
    private readonly string _identityUserId = Guid.NewGuid().ToString();
    
    [Fact]
    public void CreateUser_WhenUserValid_ShouldNewUser()
    {
        // Arrage
        var userFactory = new CreateUserFactory();

        // Act
        var user = userFactory.CreateValidUser(_identityUserId);
        var events = user.PopDomainEvents();
        var ev = events[0] as CreateUserEvent;

        //Assert
        user.Should().NotBeNull();
        user.Role.Should().Be(Roles.User);
        events.Should().HaveCount(1);
        ev.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.IdentityUserId.Should().Be(_identityUserId);

    }

    [Fact]
    public void CreateUser_WhenUserInValid_ShouldException()
    {
        // Arrage
        var userFactory = new CreateUserFactory();

        // Act
        Action userAction = () => userFactory.CreateInvalidUser(_identityUserId);

        // Assert
        userAction.Should().Throw<NoPermissionException>()
            .WithMessage("you cannot create a user with administrator rights");    
    }

    [Fact]
    public void CreateUserWithRoleManager_WhenUserValid_ShouldUser()
    {
        // Arrage
        var userFactory = new CreateUserFactory();

        // Act
        User user = userFactory.CreateUserWithRoleManager(_identityUserId);
        var events = user.PopDomainEvents();
        var ev = events[0] as CreateUserEvent;

        user.Should().NotBeNull();
        user.Role.Should().Be(Roles.Manager);
        events.Should().HaveCount(1);
        ev.Should().NotBeNull();
    }

    [Fact]
    public void LeaveTem_WhenUserIsMemberOfTeam_SlouldEmpty()
    {
        // Arrage 
        CreateUserFactory createUserFactory = new CreateUserFactory();
        var user = createUserFactory.CreateValidUser(_identityUserId);
        user.AddTeam(Guid.NewGuid(), "ff");

        // Act
        user.LeaveTeam();
        var events = user.PopDomainEvents();

        // Assert
        user.TeamId.Should().Be(Guid.Empty);
        events.Should().NotBeEmpty();
    }

    [Fact]
    public void LeaveTem_WhenUserIsNotMembersOfTeam_ShouldException()
    {
        // Arrage
        var createUserFactory = new CreateUserFactory();
        User user = createUserFactory.CreateValidUser(_identityUserId);

        // Act
        Action leaveTem = () => user.LeaveTeam();

        // Assert
        leaveTem.Should().Throw<Exception>().
            WithMessage("the user is not a member of the team");
    }

    [Fact]
    public void Delete_WhenUserNotAdmin_ShouldEmpty()
    {
        // Arrage
        var userFactory = new CreateUserFactory();
        var user = userFactory.CreateValidUser(_identityUserId);

        // Act
        user.Delete();
        List<IDomainEvent> events = user.PopDomainEvents();
        IDomainEvent @event = events[1] as DeleteUserEvent;

        // Assert
        events.Should().NotBeEmpty();
        events.Should().HaveCount(2);
        @event.Should().NotBeNull();
    } 
}
