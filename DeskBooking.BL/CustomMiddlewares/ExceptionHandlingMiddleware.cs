using DeskBooking.Domain.Common.DataModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        ContextErrorResponse response = exception switch
        {
            NotFoundException notFoundException => new ContextErrorResponse
            {
                Code = StatusCodes.Status404NotFound,
                Message = notFoundException.Message
            },

            RequestException requestException => new ContextErrorResponse
            {
                Code = requestException.Code,
                Message = requestException.Message
            },

            AuthException authException => new ContextErrorResponse
            {
                Code = authException.Code,
                Message = authException.Message
            },

            CustomValidationException validationException => new ContextErrorResponse
            {
                Code = StatusCodes.Status400BadRequest,
                Message = validationException.Message
            },

            Exception defaultException => new ContextErrorResponse
            {
                Message = defaultException.Message
            },

            _ => new ContextErrorResponse { }
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = response.Code;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
