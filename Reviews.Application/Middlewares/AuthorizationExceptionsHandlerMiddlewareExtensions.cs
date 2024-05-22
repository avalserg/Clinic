using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Reviews.Domain.Exceptions.Base;

namespace Reviews.Application.Middlewares;

internal class AuthorizationExceptionsHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationExceptionsHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedException exception)
        {
            await HandleExceptionAsync(context, exception);
        }
        catch (ForbiddenException exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;
        switch (exception)
        {
            case UnauthorizedException unauthorized:
                code = HttpStatusCode.Unauthorized;
                result = JsonSerializer.Serialize(unauthorized.Message);
                break;
            case ForbiddenException forbidden:
                code = HttpStatusCode.Forbidden;
                result = JsonSerializer.Serialize(forbidden.Message);
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}

public static class AuthorizationExceptionsHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthorizationExceptionsHandlerMiddleware>();
    }
}