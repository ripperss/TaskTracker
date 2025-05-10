using Domain.Tests.Users;
using FluentAssertions;
using TaskTracker.Domain.Tems;
using TaskTracker.Domain.Tems.Events;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users;

namespace Domain.Tests.Tems;

public class TeamTest
{
    private readonly CreateUserFactory _userFactory = new();
    private readonly string _identityUserId = Guid.NewGuid().ToString();

    [Theory]
    [InlineData("Dev Team", "secure123")]
    [InlineData("QA Team", "test456")]
    public void CreateTeam_WithValidData_ShouldSucceed(string name, string password)
    {
        // Arrange
        var manager = _userFactory.CreateUserWithRoleManager(_identityUserId);

        // Act
        var team = Team.Create(name, password, manager);

        // Assert
        team.Should().NotBeNull();
        team.Name.Should().Be(name);
        team.Members.Should().Contain(manager);
        team.AdminId.Should().Be(manager.Id);
    }

    [Theory]
    [InlineData("", "pass123")]
    [InlineData("Dev Team", "")]
    [InlineData(null, "pass123")]
    [InlineData("Dev Team", null)]
    public void CreateTeam_WithInvalidData_ShouldThrowException(string name, string password)
    {
        // Arrange
        var manager = _userFactory.CreateUserWithRoleManager(_identityUserId);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Team.Create(name, password, manager));
    }

    [Fact]
    public void CreateTeam_WithNonManagerUser_ShouldThrowException()
    {
        // Arrange
        var user = _userFactory.CreateValidUser(_identityUserId);

        // Act & Assert
        Assert.Throws<NoPermissionException>(() => Team.Create("Team", "pass", user));
    }

    [Fact]
    public void AddMember_WithCorrectPassword_ShouldAddUser()
    {
        // Arrange
        var manager = _userFactory.CreateUserWithRoleManager(_identityUserId);
        var team = Team.Create("Dev Team", "pass123", manager);
        var user = _userFactory.CreateValidUser(Guid.NewGuid().ToString());

        // Act
        team.AddMember(user, "pass123");
        var events = team.PopDomainEvents();
        var @event = events[0] as AddMembersOfTeamsEvent;

        // Assert
        team.Members.Should().HaveCount(2).And.Contain(user);
        user.TeamId.Should().Be(team.Id);
        @event.Should().NotBeNull();
    }

    [Fact]
    public void AddMember_WithIncorrectPassword_ShouldThrowException()
    {
        // Arrange
        var manager = _userFactory.CreateUserWithRoleManager(_identityUserId);
        var team = Team.Create("Dev Team", "pass123", manager);
        var user = _userFactory.CreateValidUser(Guid.NewGuid().ToString());

        // Act & Assert
        Assert.Throws<IncorrectPasswordExcepton>(() => team.AddMember(user, "wrongpass"));
    }

    [Fact]
    public void RemoveMember_WhenValid_ShouldRemoveUser()
    {
        // Arrange
        var manager = _userFactory.CreateUserWithRoleManager(_identityUserId);
        var team = Team.Create("Dev Team", "pass123", manager);
        var user = _userFactory.CreateValidUser(Guid.NewGuid().ToString());
        team.AddMember(user, "pass123");

        // Act
        team.RemoveMember(user);
        var events = team.PopDomainEvents();
        var @event = events[0] as UserRemovedFromTeamEvent;

        // Assert
        team.Members.Should().NotContain(user);
        user.TeamId.Should().BeNull();
    }

    [Fact]
    public void RemoveMember_WhenAdmin_ShouldThrowException()
    {
        // Arrange
        var manager = _userFactory.CreateUserWithRoleManager(_identityUserId);
        var team = Team.Create("Dev Team", "pass123", manager);

        // Act & Assert
        Assert.Throws<NoPermissionException>(() => team.RemoveMember(manager));
    }
}