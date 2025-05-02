using Domain.Tests.Users;
using FluentAssertions;
using TaskTracker.Domain.Tems;
using TaskTracker.Domain.Tems.Exceptions;
using TaskTracker.Domain.Users;

namespace Domain.Tests.Tems;

public class TeamTest
{
    private readonly CreateUserFactory _createUserFactory = new CreateUserFactory();

    [Theory]
    [InlineData("ggg", "Ggg")]
    [InlineData("Hrr", "Gg")]
    public void CreateTeam_WithValidData_ShouldCreateTeam(string name, string teamPassword)
    {
        // Arrage 
        var user = _createUserFactory.CreateUserWithRoleManager();
        
        // Act
        var team  = Team.Create(name, teamPassword, user);

        // Assert
        team.Should().NotBeNull();
        team.Id.Should().NotBeEmpty();


    }

    [Theory]
    [InlineData("", "Ggg")]
    [InlineData("", "")]
    public void CreateTeam_WhenInvalidData_ShouldException(string name, string teamPassword)
    {
        // Arrage
        var user = _createUserFactory.CreateUserWithRoleManager();

        // Act
        var team = () => Team.Create(name, teamPassword, user);    

        // Assert
        team.Should().Throw<NullReferenceException>();
    }

    [Fact]
    public void CreateTeam_WhenInCorrectRole_ShouldException()
    {
        // Arrage
        var user = _createUserFactory.CreateValidUser();

        // Act
        var team = () => Team.Create("f", "f", user);

        // Assert
        team.Should().Throw<NoPermissionException>()
            .WithMessage("Only users with the manager role can create teams.");
    }

    [Fact]
    public void AddMembers_WhenValidPassword_ShouldEmpty()
    {
        // Arrage
        var user = _createUserFactory.CreateUserWithRoleManager();
        var team = Team.Create("name", "password", user);

        // Act
        team.AddMember(user, "password");

        // Assert
        team.Members.Should().HaveCount(1);
        team.Members.Should().Contain(user);
    }

    [Fact]
    public void AddMembers_WhenInValidPassword_ShouldException()
    {
        // Arrage
        var admin = _createUserFactory.CreateUserWithRoleManager();
        var team = Team.Create("name", "password", admin);
        var user = _createUserFactory.CreateValidUser();

        // Act
        Action tramAddAction = () => team.AddMember(user, "InConrrectpassword");

        // Assert
        tramAddAction.Should().Throw<IncorrectPasswordExcepton>();
    }

    [Fact]
    public void RemoveMember_WhenValidData_ShouldEmpty()
    {
        // Arrage
        var admin = _createUserFactory.CreateUserWithRoleManager();
        var team = Team.Create("name", "password", admin);
        var user = _createUserFactory.CreateValidUser();
        team.AddMember(user, "password");

        // Act
        team.RemoveMember(user);

        // Assert
        team.Members.Should().HaveCount(1);
        team.Members.Should().Contain(admin);
    }

    [Fact]
    public void RemoveMember_whenUsereQuealManager_ShouldException()
    {
        // Arrage
        var admin = _createUserFactory.CreateUserWithRoleManager();
        var team = Team.Create("name", "password", admin);

        // Act
        Action removeManagerAction = () => team.RemoveMember(admin);

        // Assert
        removeManagerAction.Should().Throw<NoPermissionException>("No permission to remove this user");
    }
}
