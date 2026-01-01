using MediatR;
using UserService.Application.DTOs;
using UserService.Domain.Common.Result;

namespace UserService.Application.Features.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId)
    : IRequest<Result<UserDto>>;