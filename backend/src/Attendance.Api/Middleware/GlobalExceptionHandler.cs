using System.Net;
using System.Text.Json;

namespace Attendance.Api.Middleware;

public sealed class GlobalExceptionHandler
{
    private readonly RequestDelegate next;
    private readonly ILogger<GlobalExceptionHandler> logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Unhandled request exception for {Path}", context.Request.Path);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                status = (int)HttpStatusCode.InternalServerError,
                message = "An unexpected error occurred."
            }));
        }
    }
}
