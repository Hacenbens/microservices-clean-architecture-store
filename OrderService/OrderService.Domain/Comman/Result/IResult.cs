namespace OrderService.Domain.Comman.Result;
public interface IResult
{
    bool IsSuccess { get; }
    Error Error { get; }
}