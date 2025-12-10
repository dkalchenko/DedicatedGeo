using System.Net;
using DedicatedGeo.Mono.Bootstrap.Settings;
using FluentValidation;
using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DedicatedGeo.Mono.Bootstrap.Middlewares;

public class ErrorHandlerMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = Guard.ThrowIfNull(next, nameof(next));
        _logger = Guard.ThrowIfNull(logger, nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context, AppSettings appSettings)
    {
        try
        {
            context.Request.EnableBuffering();
            await _next(context);
        }
        catch (Exception ex)
        {
            HandleException(context, ex, appSettings);
        }
    }

    private void HandleException(HttpContext context, Exception ex, AppSettings appSettings)
    {
        Guard.ThrowIfNull(appSettings, nameof(appSettings));
        var exception = new ExceptionResponse();

        switch (ex)
        {
            case ValidationException e:
                exception.Message = e.Errors.First().ErrorMessage;
                exception.StatusCode = HttpStatusCode.BadRequest;
                break;
            case RequestException e:
                exception.Message = e.Message;
                exception.StatusCode = e.StatusCode ?? HttpStatusCode.InternalServerError;
                exception.StackTrace = appSettings.isProd ? "" : e.StackTrace;
                exception.ErrorCode = e.ErrorCode;
                break;
            default:
                exception.Message = ex.Message;
                exception.StatusCode = HttpStatusCode.InternalServerError;
                exception.StackTrace = ex.StackTrace;
                break;
        }

        _logger.LogError(ex, OwnConstants.ErrorTemplates.RequestExceptionMessage, exception.StatusCode,
            exception.Message);

        // if (appSettings.isProd && exception.StatusCode >= HttpStatusCode.InternalServerError)
        // {
        //     exception.Message = "Oops, something happened";
        //     exception.StackTrace = "";
        // }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)exception.StatusCode;
        context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
    }
    
    public static string GetRequestBody(HttpContext context)
    {
        var bodyStream = new StreamReader(context.Request.Body);
        bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
        var bodyText = bodyStream.ReadToEndAsync().Result;
        context.Request.Body.Position = 0;
        return bodyText;
    }
}