using FluentAssertions;
using TaskTracker.Domain.Common;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users;
using TaskTracker.Domain.Users.Event;

namespace Domain.Tests.Users;

public sealed class UserTest
{
    private readonly string _identityUserId = Guid.NewGuid().ToString();

    [Fact]
    public void CreateUser_WhenValid_ShouldReturnNewUser()
    {
        // Arrange
        var userFactory = new CreateUserFactory();

        // Act
        var user = userFactory.CreateValidUser(_identityUserId);
        var events = user.PopDomainEvents();
        var ev = events[0] as CreateUserEvent;

        // Assert
        user.Should().NotBeNull();
        user.Role.Should().Be(Roles.User);
        events.Should().HaveCount(1);
        ev.Should().NotBeNull();
        user.Id.Should().NotBeEmpty();
        user.IdentityUserId.Should().Be(_identityUserId);
    }

    [Fact]
    public void CreateUser_WhenAdminRole_ShouldThrowException()
    {
        // Arrange
        var userFactory = new CreateUserFactory();

        // Act
        Action action = () => userFactory.CreateInvalidUser(_identityUserId);

        // Assert
        action.Should().Throw<NoPermissionException>()
            .WithMessage("you cannot create a user with administrator rights");
    }

    [Fact]
    public void CreateUser_WithManagerRole_ShouldSucceed()
    {
        // Arrange
        var userFactory = new CreateUserFactory();

        // Act
        var user = userFactory.CreateUserWithRoleManager(_identityUserId);
        var events = user.PopDomainEvents();

        // Assert
        user.Role.Should().Be(Roles.Manager);
        events.Should().ContainSingle(e => e is CreateUserEvent);
    }

    [Fact]
    public void AssignToTeam_WhenNotInTeam_ShouldSetTeamId()
    {
        // Arrange
        var user = new CreateUserFactory().CreateValidUser(_identityUserId);
        var teamId = Guid.NewGuid();

        // Act
        user.JoinTeam(teamId);

        // Assert
        user.TeamId.Should().Be(teamId);
    }

    [Fact]
    public void AssignToTeam_WhenAlreadyInTeam_ShouldThrowException()
    {
        // Arrange
        var user = new CreateUserFactory().CreateValidUser(_identityUserId);
        user.JoinTeam(Guid.NewGuid());

        // Act
        Action action = () => user.JoinTeam(Guid.NewGuid());

        // Assert
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("User is already in a team.");
    }

    [Fact]
    public void LeaveTeam_WhenInTeam_ShouldClearTeamId()
    {
        // Arrange
        var user = new CreateUserFactory().CreateValidUser(_identityUserId);
        user.JoinTeam(Guid.NewGuid());

        // Act
        user.LeaveTeam();

        // Assert
        user.TeamId.Should().BeNull();
    }

    [Fact]
    public void LeaveTeam_WhenNotInTeam_ShouldThrowException()
    {
        // Arrange
        var user = new CreateUserFactory().CreateValidUser(_identityUserId);

        // Act
        Action action = () => user.LeaveTeam();

        // Assert
        action.Should().Throw<Exception>()
            .WithMessage("the user is not a member of the team");
    }

    [Fact]
    public void Delete_WhenNotAdmin_ShouldRaiseDeleteEvent()
    {
        // Arrange
        var user = new CreateUserFactory().CreateValidUser(_identityUserId);

        // Act
        user.Delete();
        var events = user.PopDomainEvents();

        // Assert
        events.Should().ContainSingle(e => e is DeleteUserEvent);
    }
}