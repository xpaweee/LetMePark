using System.Text.Json;
using Humanizer;
using LetMePark.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace LetMePark.Infrastructure.Exceptions;

internal sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.ToString());
            await HandleExceptionAsync(exception, context);
        }
    }

    private async Task HandleExceptionAsync(Exception ex, HttpContext context)
    {
        var (statusCode, error) = ex switch
        {
            CustomException => (StatusCodes.Status400BadRequest, new Error(ex.GetType().Name.Underscore().Replace("_exception", string.Empty), ex.Message)),
            _ => (StatusCodes.Status500InternalServerError, new Error("error", "There was an error"))
        };

        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }

    private record Error(string Code, string Reason);
}