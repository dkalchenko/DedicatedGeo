using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Dtos.Location;
using HybridModelBinding;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Device;

[Route("/v1/public/device")]
public class DevicePublicController: Controller
{
    private readonly ISender _sender;

    public DevicePublicController(ISender sender)
    {
        _sender = sender.ThrowIfNull();
    }

    [HttpGet]
    public async Task<IActionResult> PostPoints([FromHybrid] GetDeviceStatusPublicRequest request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpPut]
    public async Task<IActionResult> PostPoints([FromHybrid] PutDeviceStatusPublicRequest request)
    {
        await _sender.Send(request);
        return Ok();
    }
}