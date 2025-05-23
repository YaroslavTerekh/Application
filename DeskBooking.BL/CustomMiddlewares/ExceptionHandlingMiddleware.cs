using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DeskBooking.BL.CustomMiddlewares;

public class ExceptionHandlingMiddleware
{
    private RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception ex) 
        {
            await HandleErrorAsync(ex, context);
        }
    }

    private async Task HandleErrorAsync(Exception exception, HttpContext context)
    {
        var response = new
        {
            message = exception.Message,
            code = 500
        };

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
