using System.Net;
using System.Text.Json;
using JetStreamData.Kernel.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JetStreamData.Kernel.AspNet.Filters;

public sealed class HttpGlobalOrderProcessingExceptionMiddleware(RequestDelegate nextRequestDelegate, ILogger<HttpGlobalOrderProcessingExceptionMiddleware> requestLogger)
{
    private static async Task HandleApplicationDomainExceptionAsync(HttpContext httpRequestContext, Exception applicationDomainException)
    {
        var validationProblemDetails = new ValidationProblemDetails
        {
            Instance = httpRequestContext.Request.Path, Status = StatusCodes.Status400BadRequest, Detail = "Please refer to the errors property for additional details."
        };

        validationProblemDetails.Errors.Add("DomainValidations", new[] { applicationDomainException.Message });
        httpRequestContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        await WriteProblemDetailsResponse(httpRequestContext, validationProblemDetails);
    }

    private async Task HandleNonDomainExceptionAsync(HttpContext httpRequestContext, Exception generalException)
    {
        requestLogger.LogError(new EventId(generalException.HResult), generalException, generalException.Message);
        var validationProblemDetails = new ValidationProblemDetails
        {
            Instance = httpRequestContext.Request.Path, Status = StatusCodes.Status500InternalServerError, Detail = "Please refer to the errors property for additional details."
        };

        validationProblemDetails.Errors.Add("Message", [generalException.Message]);
        httpRequestContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await WriteProblemDetailsResponse(httpRequestContext, validationProblemDetails);
    }

    private static async Task WriteProblemDetailsResponse(HttpContext httpRequestContext, ValidationProblemDetails validationProblemDetails)
    {
        httpRequestContext.Response.ContentType = "application/problem+json";
        var serializedResponse = JsonSerializer.Serialize(validationProblemDetails);
        await httpRequestContext.Response.WriteAsync(serializedResponse);
    }

    public async Task Invoke(HttpContext httpRequestContext)
    {
        try
        {
            await nextRequestDelegate(httpRequestContext);
        }
        catch (DomainException applicationDomainException)
        {
            await HandleApplicationDomainExceptionAsync(httpRequestContext, applicationDomainException);
        }
        catch (Exception generalException)
        {
            await HandleNonDomainExceptionAsync(httpRequestContext, generalException);
        }
    }
}
