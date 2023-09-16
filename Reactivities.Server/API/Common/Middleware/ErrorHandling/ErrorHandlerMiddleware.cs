using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Common.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Reactivities.Common.ErrorHandling.Models;

namespace API.Common.Middleware.ErrorHandling;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _environment;

    public ErrorHandlerMiddleware(RequestDelegate next, IHostEnvironment environment)
    {
        this._next = next;
        this._environment = environment;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this._next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            response.StatusCode = ex switch
            {
                AppException => (int)HttpStatusCode.BadRequest,
                KeyNotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var exceptionResponse = this._environment.IsDevelopment()
                ? ExceptionResponseModel.New(context.Response.StatusCode, ex.Message, ex.StackTrace)
                : ExceptionResponseModel.New(response.StatusCode, CommonErrorMessages.SomethingWentWrong);

            await response.WriteAsync(JsonSerializer.Serialize(exceptionResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }
}