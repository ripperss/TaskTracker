using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Application.Auth.Commands.Register;
using TaskTracker.Application.Common.Interfaces;
using TaskTracker.Application.Common.Models;
using TaskTracker.Domain.Users;

namespace Application.Tests.Auth.Command;

public class RegisterUserCommandTest
{
    private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
    private readonly Mock<IUserApplicationService> _userAppServiceMock = new Mock<IUserApplicationService>();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
    private readonly Mock<ILogger<UserRegisterCommandHandler>> _loggerMock = new Mock<ILogger<UserRegisterCommandHandler>>();
    private readonly UserRegisterCommandHandler _handler;

    public RegisterUserCommandTest()
    {
        _handler = new UserRegisterCommandHandler(
            _userRepositoryMock.Object
            , _userAppServiceMock.Object
            , _unitOfWorkMock.Object
            , _loggerMock.Object);
    }

    [Fact]
    public async Task RegisterUser_WhenUserValid_ShouldBeEmpty()
    {
        // Arrage
        var request = new UserRegisterCommand("Jhon","Miller","rippergods@gmail.com","aiaia");
      
        var identityResponse = new UserResponseRegisterDto
        {
            UserIdentityId = "123",
            Email = request.Email,
            FirstName = request.FirstName,
            Role = Roles.User
        };

        _userAppServiceMock
            .Setup(x => x.RegisterAsync(request))
            .ReturnsAsync(identityResponse);

        var user = User.Create("123", Roles.User);
        _userRepositoryMock
            .Setup(x => x.CreateUserAsync(user))
            .Returns(Task.FromResult(user));

        _unitOfWorkMock
            .Setup(x => x.CommitChangesAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.Equal(request.Email, result.Email);
        Assert.Equal(Roles.User, result.Role);

        _userAppServiceMock.Verify(x => x.RegisterAsync(request), Times.Once);
        _userRepositoryMock.Verify(x => x.CreateUserAsync(It.IsAny<User>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitChangesAsync(), Times.Once);
    }
}
