namespace UserService.Application.DTOs;

public sealed record UserDto(
    Guid Id,
    string Email,
    string Name,
    DateTime CreatedAt);