using System.Net;
using Microsoft.AspNetCore.Mvc;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                HttpStatusCode.NotFound);
        }
        catch (ConflictException ex)
        {
            await HandleExceptionAsync(
                context,
                ex,
                HttpStatusCode.Conflict);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred.");
            await HandleExceptionAsync(
                context,
                ex,
                HttpStatusCode.InternalServerError);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception,
        HttpStatusCode statusCode)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/problem+json";

        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = statusCode switch
            {
                HttpStatusCode.NotFound => "Reseource Not Found",
                HttpStatusCode.Conflict => "Conflict",
                _ => "Internal Server Error"
            },
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}