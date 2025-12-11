using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Dtos.Location;
using HybridModelBinding;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.S2D;

[Route("/v1/S2D/devices")]
public class DeviceS2DController: Controller
{
    private readonly ISender _sender;
    public DeviceS2DController(ISender sender)
    {
        _sender = sender.ThrowIfNull();
    }
    
    [HttpPut("{deviceId}/status")]
    public async Task<IActionResult> PutStatus([FromHybrid] PutDeviceStatusS2DRequest request)
    {
        await _sender.Send(request);
        return Ok();
    }
    
    [HttpPost("{deviceId}/location/points")]
    public async Task<IActionResult> PostPoints([FromHybrid] PostLocationPointsS2DRequest request)
    {
        await _sender.Send(request);
        return Ok();
    }
}