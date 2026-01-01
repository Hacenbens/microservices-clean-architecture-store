using UserService.Domain.Common.IEntities;
using UserService.Domain.Common.Result;

namespace UserService.Domain.Entities;

public sealed class User : AuditableEntity
{
    public string Email { get; private set; } = default!;
    public string Name { get; private set; } = default!;

    private User() { }

    private User(string email, string name)
    {
        Email = email;
        Name = name;
    }

    public static Result<User> Create(string email, string name)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result<User>.Failure(
                new Error(
                    "User.Email.Empty",
                    "Email is required",
                    ErrorKind.Validation));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<User>.Failure(
                new Error(
                    "User.Name.Empty",
                    "Name is required",
                    ErrorKind.Validation));
        }

        return Result<User>.Success(new User(email, name));
    }

    public void UpdateName(string name)
    {
        Name = name;
        MarkUpdated();
    }
}