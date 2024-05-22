using System.Net;
using Authorization.Application.Exceptions;
using FluentValidation;

namespace Authorization.Api.Middlewares;

internal class CoreExceptionsHandlerMiddleware
{
    private readonly RequestDelegate _next;
    

    public CoreExceptionsHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
        
    }
    public async Task Invoke(HttpContext context, ILogger<CoreExceptionsHandlerMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception, logger);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger<CoreExceptionsHandlerMiddleware> logger)
    {
        HttpStatusCode code = HttpStatusCode.InternalServerError;
        string result = string.Empty;
        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = System.Text.Json.JsonSerializer.Serialize(validationException.Errors);
                break;
            case BadOperationException badOperationException:
                code = HttpStatusCode.BadRequest;
                result = System.Text.Json.JsonSerializer.Serialize(badOperationException.Message);
                break;
            case NotFoundException notFound:
                code = HttpStatusCode.NotFound;
                result = System.Text.Json.JsonSerializer.Serialize(notFound.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = System.Text.Json.JsonSerializer.Serialize(new {error = exception.Message, innerMessage = exception.InnerException?.Message, exception.StackTrace});
        }

        logger.Log(code == HttpStatusCode.InternalServerError ? LogLevel.Error : LogLevel.Warning, exception, $"Response error {code}: {exception.Message}");

        return context.Response.WriteAsync(result);
    }
}

public static class CoreExceptionsHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCoreExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CoreExceptionsHandlerMiddleware>();
    }
}
