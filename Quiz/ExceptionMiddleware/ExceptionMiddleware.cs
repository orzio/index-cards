using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var exceptionType = exception.GetType();
        switch (exception)
        {
            case Exception e when exceptionType == typeof(UnauthorizedAccessException):
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                break;
        }

        var payload = JsonConvert.SerializeObject(exception.Message);

        await context.Response.WriteAsync(payload);
    }
}