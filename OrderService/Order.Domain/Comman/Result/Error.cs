namespace ProductService.Domain.Comman.Result;
public sealed record Error(
    string Code,
    string Message,
    ErrorKind Kind)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorKind.None);
}