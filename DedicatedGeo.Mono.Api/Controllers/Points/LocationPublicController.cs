using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dtos.Location;
using HybridModelBinding;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Points;

[Route("/v1/public/location")]
public class LocationPublicController: Controller
{
    private readonly ISender _sender;

    public LocationPublicController(ISender sender)
    {
        _sender = sender.ThrowIfNull();
    }

    [HttpPost("points")]
    public async Task<IActionResult> PostPoints([FromHybrid] PostLocationPointsPublicRequest request)
    {
        await _sender.Send(request);
        return Ok();
    }

    [HttpDelete("points")]
    public async Task<IActionResult> DeletePoints([FromHybrid] DeleteLocationPointsPublicRequest request)
    {
        await _sender.Send(request);
        return Ok();
    }
}