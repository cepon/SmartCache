using SmartCache.Domain.Exceptions;
using SmartCache.Domain.Models;
using System.Net;
using System.Text.Json;

namespace SmartCache.Host.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;

    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
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
        catch (CustomException ex)
        {
            _logger.LogError(ex, "{ErrorMessage}", ex.Message);
            await HandleCustomExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{ErrorMessage}", ex.Message);
            await HandleUnknownExceptionAsync(context);
        }
    }

    private static Task HandleCustomExceptionAsync(HttpContext context, CustomException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)exception.StatusCode;

        var result = JsonSerializer.Serialize(new CustomExceptionResponse
        {
            Message = exception.Message
        });

        return context.Response.WriteAsync(result);
    }

    private static Task HandleUnknownExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var result = JsonSerializer.Serialize(new CustomExceptionResponse
        {
            Message = "An error occurred while processing the request."
        });

        return context.Response.WriteAsync(result);
    }
}
