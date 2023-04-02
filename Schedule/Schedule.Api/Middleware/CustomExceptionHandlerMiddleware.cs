using System.Net;
using FluentValidation;
using Newtonsoft.Json;
using Schedule.Core.Common.Exceptions;

namespace Schedule.Api.Middleware;

public sealed class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (exception)
        {
            case UnauthorizedAccessException:
                code = HttpStatusCode.Unauthorized;
                break;
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(validationException.Errors);
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (string.IsNullOrEmpty(result))
            result = JsonConvert.SerializeObject(new { error = exception.Message });

        /*Log.Error(exception, "User: {@Name} \n{@Exception}",
            context.User.Identity?.Name ?? "Unauthorized", exception);*/

        return context.Response.WriteAsync(result);
    }
}