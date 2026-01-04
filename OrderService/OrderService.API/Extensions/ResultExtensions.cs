using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Comman.Result;

namespace OrderService.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToProblem(this Error error)
    {
        var status = error.Kind switch
        {
            ErrorKind.NotFound => StatusCodes.Status404NotFound,
            ErrorKind.Validation => StatusCodes.Status400BadRequest,
            ErrorKind.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        return new ObjectResult(new ProblemDetails
        {
            Status = status,
            Title = error.Code,
            Detail = error.Message
        })
        {
            StatusCode = status
        };
    }
}