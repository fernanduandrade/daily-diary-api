using System.Net;
using System.Text.Json;

namespace DailyDiary.API.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
        => (_next) = (next);

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionResponse(ex, context);
        }
    }

    private async Task HandleExceptionResponse(Exception exception, HttpContext context)
    {
        var result = JsonSerializer.Serialize(new { error = exception.Message  });
        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await response.WriteAsync(JsonSerializer.Serialize(result));
    }
}