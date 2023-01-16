using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using Server.Contracts.Exceptions;

namespace Server.Infrastructure.Middlewares;

public sealed class ExceptionHandlingMiddleware 
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
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
        catch (ApiException ex)
        {
            ProblemDetails problemDetails = new()
            {
                Status = ex.StatusCode,
                Type = ex.GetType().Name,
                Title = ex.Message,
                Detail = ex.Description
            };
           _logger.Write(ex.GetLevel(), ex,JsonSerializer.Serialize(problemDetails), ex.Description);
           await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.Fatal(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, ApiException ex)
    {
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsJsonAsync(ex.ToString());;
    }
    
    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsJsonAsync(
            new ApiException(ex.Message, context.Response.StatusCode, LogEventLevel.Fatal).ToString());
    }
}