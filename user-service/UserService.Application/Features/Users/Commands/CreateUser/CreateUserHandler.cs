using MediatR;
using UserService.Application.Abstractions.Irepositories;
using UserService.Application.DTOs;
using UserService.Domain.Common.Result;
using UserService.Domain.Entities;

namespace UserService.Application.Features.Users.Commands.CreateUser;

public sealed class CreateUserHandler
    : IRequestHandler<CreateUserCommand, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var userResult = User.Create(request.Email, request.Name);
        if (!userResult.IsSuccess)
            return Result<UserDto>.Failure(userResult.Error);

        var user = userResult.Value!;

        await _userRepository.AddAsync(user, cancellationToken);

        return Result<UserDto>.Success(
            new UserDto(
                user.Id,
                user.Email,
                user.Name,
                user.CreatedAt));
    }
}