using System.Net;
using JetStreamData.Kernel.AspNet.Results;
using JetStreamData.Kernel.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace JetStreamData.Kernel.AspNet.Filters;

public class HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger) : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        logger.LogError(new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

        if (context.Exception is DomainException)
        {
            var problemDetails = new ValidationProblemDetails
            {
                Instance = context.HttpContext.Request.Path, Status = StatusCodes.Status400BadRequest, Detail = "Please refer to the errors property for additional details."
            };

            problemDetails.Errors.Add("DomainValidations", new[] { context.Exception.Message });
            context.Result = new BadRequestObjectResult(problemDetails);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        else
        {
            var problemDetails = new ValidationProblemDetails
            {
                Instance = context.HttpContext.Request.Path,
                Status = StatusCodes.Status500InternalServerError,
                Detail = "Please refer to the errors property for additional details."
            };

            problemDetails.Errors.Add("Message", new[] { context.Exception.Message });
            context.Result = new InternalServerErrorObjectResult(problemDetails);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
