using MediatR;
using UserService.Application.DTOs;
using UserService.Domain.Common.Result;

namespace UserService.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string Email,
    string Name
) : IRequest<Result<UserDto>>;