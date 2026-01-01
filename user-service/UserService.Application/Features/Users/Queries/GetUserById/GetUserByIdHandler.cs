using MediatR;
using UserService.Application.Abstractions.Irepositories;
using UserService.Application.DTOs;
using UserService.Domain.Common.Result;

namespace UserService.Application.Features.Users.Queries.GetUserById;

public sealed class GetUserByIdHandler
    : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> Handle(
        GetUserByIdQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result<UserDto>.Failure(
                new(
                    "User.NotFound",
                    "User not found",
                    ErrorKind.NotFound));
        }

        return Result<UserDto>.Success(
            new UserDto(
                user.Id,
                user.Email,
                user.Name,
                user.CreatedAt));
    }
}