using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetStreamData.Kernel.AspNet.Results;

public sealed class InternalServerErrorObjectResult : ObjectResult
{
    public InternalServerErrorObjectResult(object error) : base(error)
    {
        StatusCode = StatusCodes.Status500InternalServerError;
    }
}
