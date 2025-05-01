using FluentAssertions;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users;
using TaskTracker.Domain.Users.Event;

namespace Domain.Tests.Users;

sealed public class UserTest
{
    [Fact]
    public void CreateUser_WhenUserValid_ShouldNewUser()
    {
        // Arrage
        var userFactory = new CreateUserFactory();

        // Act
        var user = userFactory.CreateValidUser();
        var events = user.PopDomainEvents();
        var ev = events[0] as CreateUserEvent;

        //Assert
        user.Should().NotBeNull();
        user.Role.Should().Be(Roles.User);
        events.Should().HaveCount(1);
        ev.Should().NotBeNull();

    }

    [Fact]
    public void CreateUser_WhenUserInValid_ShouldException()
    {
        // Arrage
        var userFactory = new CreateUserFactory();

        // Act
        Action userAction = () => userFactory.CreateInvalidUser();

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
        User user = userFactory.CreateUserWithRoleManager();
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
        var user = createUserFactory.CreateValidUser();
        user.AddTeam(1);

        // Act
        user.LeaveTem();
        var events = user.PopDomainEvents();
        var ev = events[1] as UserLeftTeamEvent;

        // Assert
        user.TeamId.Should().Be(0);
        events.Should().NotBeEmpty();
        ev.Should().NotBeNull();
    }

    [Fact]
    public void LeaveTem_WhenUserIsNotMembersOfTeam_ShouldException()
    {
        // Arrage
        var createUserFactory = new CreateUserFactory();
        var user = createUserFactory.CreateValidUser();

        // Act
        Action leaveTem = () => user.LeaveTem();

        // Assert
        leaveTem.Should().Throw<Exception>().
            WithMessage("the user is not a member of the team");
    }
}
