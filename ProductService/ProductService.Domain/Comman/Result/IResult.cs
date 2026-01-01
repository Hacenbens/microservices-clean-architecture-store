namespace UserService.Domain.Common.Result;
public interface IResult
{
    bool IsSuccess { get; }
    Error Error { get; }
}