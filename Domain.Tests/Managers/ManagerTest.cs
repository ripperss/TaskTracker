using Domain.Tests.Users;
using FluentAssertions;
using TaskTracker.Domain.Managers;
using TaskTracker.Domain.Managers.Events;
using TaskTracker.Domain.Tems;

namespace Domain.Tests.Managers;

public class ManagerTest
{
    private readonly CreateUserFactory userFactory = new CreateUserFactory();
    private readonly string _identityUserId = Guid.NewGuid().ToString();

    [Fact]
    public void CreateManager_WhenValidData_ShouldEmpty()
    {
        // Arrage
        var user = userFactory.CreateUserWithRoleManager(_identityUserId);
        var team = Team.Create("name", "teamPassword", user);

        // Act
        var manager = Manager.Create(user, team);
        var events = manager.PopDomainEvents();
        var ev = events[0] as ManagerCreatedEvent;

        // Assert
        manager.Should().NotBeNull();
        manager.Id.Should().NotBeEmpty();
        events.Should().HaveCount(1);
        ev.Should().NotBeNull();
    }

    [Fact]
    public void RemoveMemberOfTeams_WhenUserNotQueueManager_ShouldEmpty()
    {
        // Arrage
        var user = userFactory.CreateUserWithRoleManager(_identityUserId);
        var team = Team.Create("name", "teamPassword", user);
        var manager = Manager.Create(user, team);
        var testUser = userFactory.CreateValidUser(_identityUserId);
        testUser.AddTeam(team.Id, "teamPassword");

        // Act
        manager.RemoveMemberOfTeams(testUser);
        var events = manager.PopDomainEvents();
        var ev = events[1];

        // Assert
       events.Should().NotBeNull();
       events.Should().HaveCount(2);
       ev.Should().NotBeNull();

    }
}
