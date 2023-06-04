using System.Net;
using FluentValidation;
using Newtonsoft.Json;
using Schedule.Core.Common.Exceptions;
using Serilog;

namespace Schedule.Api.Middleware.CustomException;

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
        var result = "Неизвестная ошибка.";
        
        switch (exception)
        {
            case UnauthorizedAccessException:
                code = HttpStatusCode.Unauthorized;
                result = "Нет авторизован.";
                break;
            case ValidationException:
                code = HttpStatusCode.BadRequest;
                result = "Некорректный запрос.";
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                result = "Не найдено.";
                break;
            case AuthorizationException:
                code = HttpStatusCode.BadRequest;
                result = "Неверный логин или пароль.";
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        Log.Error(exception, "{@Exception}", exception);

        return context.Response.WriteAsync( 
            JsonConvert.SerializeObject(new
            {
                error = result
            }));
    }
}