using Asp.Versioning;
using JetStreamData.FlightsService.Application.Queries.Models;
using JetStreamData.Kernel.Api;
using JetStreamData.Kernel.Dispatcher;
using JetStreamData.Kernel.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JetStreamData.FlightsService.Presentation.Controllers;

[ApiController]
[ApiVersionNeutral]
[Route("api/v{version:apiVersion}/flights")]
public sealed class FlightController(IDispatcher dispatcher) : BaseController(dispatcher)
{
    [HttpGet]
    public async Task<ActionResult> Get([FromQuery] SearchFlights.Query query)
    {
        return await Dispatcher
            .DispatchAsync(query, HttpContext.RequestAborted)
            .ToActionResultAsync();
    }
}
