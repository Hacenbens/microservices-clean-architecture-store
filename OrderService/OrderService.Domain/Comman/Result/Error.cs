namespace OrderService.Domain.Comman.Result;
public sealed record Error(
    string Code,
    string Message,
    ErrorKind Kind)
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorKind.None);
    public static Error NotFound(string code, string message) =>
        new(code, message, ErrorKind.NotFound);

    public static Error Validation(string code, string message) =>
        new(code, message, ErrorKind.Validation);

    public static Error Conflict(string code, string message) =>
        new(code, message, ErrorKind.Conflict);

    public static Error Failure(string code, string message) =>
        new(code, message, ErrorKind.Failure);
}