using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Managers;
using TaskTracker.Domain.TasksUser;
using TaskTracker.Domain.Tems;
using TaskTracker.Domain.Users;

namespace Domain.Tests.Task;

public class UserTaskTest
{
    private const string TEAM_NAME = "team Name";
    private const string TEAM_PASSWORD = "password";    

    private readonly Manager _manager;
    private readonly User _user;
    private readonly Team _team;

    public UserTaskTest()
    {
        _user = User.Create(Guid.NewGuid().ToString(), Roles.Manager);
        _team = Team.Create(TEAM_NAME,TEAM_PASSWORD, _user);

        _manager = Manager.Create(_user, _team);
    }

    [Fact]
    public void Create_ValidData_ReturnsTaskWithCorrectProperties()
    {
        // Arrange
        var title = "Test Task";
        var description = "Test Description";

        // Act
        var task = Tasks.Create(title, description, _manager);

        // Assert
        task.Title.Should().Be(title);
        task.Description.Should().Be(description);
        task.Status.Should().Be(Status.News);
        task.ManagerId.Should().Be(_manager.Id);
        task.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Create_EmptyTitle_ThrowsException()
    {
        // Arrange
        var title = "";
        var description = "Test Description";

        // Act & Assert
        Assert.Throws<Exception>(() => Tasks.Create(title, description, _manager));
    }
}
