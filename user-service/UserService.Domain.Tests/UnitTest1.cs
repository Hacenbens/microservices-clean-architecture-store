using UserService.Domain.Entities;
using UserService.Domain.Common.Result;
using Xunit;

namespace UserService.Domain.Tests;

public class UserTests
{
    [Fact]
    public void Create_WithValidData_ShouldReturnSuccessResult()
    {
        // Act
        Result<User> result = User.Create("test@test.com", "Test");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(Error.None, result.Error);
        Assert.NotNull(result.Value);
        Assert.Equal("test@test.com", result.Value!.Email);
        Assert.Equal("Test", result.Value.Name);
    }

    [Fact]
    public void Create_WithEmptyEmail_ShouldReturnFailureResult()
    {
        // Act
        Result<User> result = User.Create("", "Test");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("User.Email.Empty", result.Error.Code);
        Assert.Equal(ErrorKind.Validation, result.Error.Kind);
        Assert.Null(result.Value);
    }

    [Fact]
    public void Create_WithEmptyName_ShouldReturnFailureResult()
    {
        var result = User.Create("test@test.com", "");

        Assert.False(result.IsSuccess);
        Assert.Equal("User.Name.Empty", result.Error.Code);
        Assert.Null(result.Value);
    }

    [Fact]
    public void UpdateName_ShouldChangeNameAndSetUpdatedAt()
    {
        var user = User.Create("test@test.com", "Old").Value!;

        user.UpdateName("New");

        Assert.Equal("New", user.Name);
        Assert.NotNull(user.UpdatedAt);
    }
}