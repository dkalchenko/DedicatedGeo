using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Dtos.Location;
using HybridModelBinding;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Admin;

[Route("/v1/admin/devices")]
public class DeviceAdminController: Controller
{
    private readonly ISender _sender;
    public DeviceAdminController(ISender sender)
    {
        _sender = sender.ThrowIfNull();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetDevices([FromHybrid] GetDevicesAdminRequest request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpGet("{deviceId}/status")]
    public async Task<IActionResult> GetStatus([FromHybrid] GetDeviceStatusAdminRequest request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpGet("{deviceId}/location/points")]
    public async Task<IActionResult> GetPoints([FromHybrid] GetLocationPointsAdminRequest request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpDelete("{deviceId}/location/points")]
    public async Task<IActionResult> DeletePoints([FromHybrid] DeleteLocationPointsAdminRequest request)
    {
        await _sender.Send(request);
        return Ok();
    }
    
}