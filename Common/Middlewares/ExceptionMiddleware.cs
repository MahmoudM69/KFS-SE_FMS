using Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Common.Middlewares;
public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger) =>
        _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            if (!context.Response.HasStarted)
            {
                var model = new UnhandledExceptionModel(ex);
                var response = new ObjectResult(model)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsync(response.SerializeJson());
            }
        }
    }
}

internal class UnhandledExceptionModel
{
    private Exception ex;
    private string message;
    private string? stackTrace;

    public UnhandledExceptionModel(Exception ex)
    {
        this.ex = ex;
        message = ex.Message;
        stackTrace = ex.StackTrace;
    }

    public override string ToString()
    {
        return ex.ToString();
    }
}
