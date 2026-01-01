using Moq;
using Xunit;
using UserService.Application.Features.Users.Commands.CreateUser;
using UserService.Application.Abstractions.Irepositories;
using UserService.Domain.Common.Result;
using UserService.Domain.Entities;

namespace UserService.Application.Tests;

public class CreateUserHandlerTests
{
    private readonly Mock<IUserRepository> _repositoryMock = new();

    [Fact]
    public async Task Handle_WithValidCommand_ShouldReturnSuccessResult()
    {
        // Arrange
        var handler = new CreateUserHandler(_repositoryMock.Object);
        var command = new CreateUserCommand("test@test.com", "Test");

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(Error.None, result.Error);
        Assert.NotNull(result.Value);

        _repositoryMock.Verify(
            r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidEmail_ShouldReturnFailureResult()
    {
        var handler = new CreateUserHandler(_repositoryMock.Object);
        var command = new CreateUserCommand("", "Test");

        var result = await handler.Handle(command, default);

        Assert.False(result.IsSuccess);
        Assert.Equal("User.Email.Empty", result.Error.Code);

        _repositoryMock.Verify(
            r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}