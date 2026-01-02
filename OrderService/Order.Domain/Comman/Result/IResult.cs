namespace ProductService.Domain.Comman.Result;
public interface IResult
{
    bool IsSuccess { get; }
    Error Error { get; }
}