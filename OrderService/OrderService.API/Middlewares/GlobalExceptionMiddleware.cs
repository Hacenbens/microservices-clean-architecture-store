using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Comman.Exceptions;
using OrderService.Domain.Comman.Result;
using System.Text.Json;

namespace OrderService.API.Middlewares;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            await WriteProblemDetails(
                context,
                HttpStatusCode.BadRequest,
                "Domain error",
                ex.Message);
        }
        catch (Exception ex)
        {
            await WriteProblemDetails(
                context,
                HttpStatusCode.InternalServerError,
                "Unexpected error",
                ex.Message);
        }
    }

    private static async Task WriteProblemDetails(
        HttpContext context,
        HttpStatusCode status,
        string title,
        string detail)
    {
        var problem = new ProblemDetails
        {
            Status = (int)status,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = problem.Status.Value;

        await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
    }
}