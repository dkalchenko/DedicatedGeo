using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Dtos.Location;
using HybridModelBinding;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DedicatedGeo.Mono.Api.Controllers.Device;

[Authorize]
[Route("/v1/admin/devices")]
public class DeviceAdminController: Controller
{
    private readonly ISender _sender;
    public DeviceAdminController(ISender sender)
    {
        _sender = sender.ThrowIfNull();
    }
    
    [HttpGet]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin},{OwnConstants.Roles.Admin},{OwnConstants.Roles.DeviceUser}")]
    public async Task<IActionResult> GetDevices([FromHybrid] GetDevicesAdminRequest request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpGet]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin},{OwnConstants.Roles.Admin}")]
    public async Task<IActionResult> GetDevice([FromHybrid] GetDeviceAdminRequest request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpPost]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin},{OwnConstants.Roles.Admin}")]
    public async Task<IActionResult> PostDevice([FromHybrid] PostDeviceAdminRequest request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpPut("{deviceId}")]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin},{OwnConstants.Roles.Admin}")]
    public async Task<IActionResult> PutDevice([FromHybrid] PutDeviceAdminRequest request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpDelete("{deviceId}")]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin}")]
    public async Task<IActionResult> DeleteDevice([FromHybrid] DeleteDeviceAdminRequest request)
    {
        await _sender.Send(request);
        return Ok();
    }
    
    [HttpGet("{deviceId}/statuses")]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin},{OwnConstants.Roles.Admin},{OwnConstants.Roles.DeviceUser}")]
    public async Task<IActionResult> GetStatuses([FromHybrid] GetDeviceStatusesAdminRequest request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpGet("{deviceId}/statuses/{statusName}/history")]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin},{OwnConstants.Roles.Admin},{OwnConstants.Roles.DeviceUser}")]
    public async Task<IActionResult> GetStatusHistory([FromHybrid] GetDeviceStatusHistoryAdminRequest request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpGet("{deviceId}/location/points")]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin},{OwnConstants.Roles.Admin},{OwnConstants.Roles.DeviceUser}")]
    public async Task<IActionResult> GetPoints([FromHybrid] GetLocationPointsAdminRequest request)
    {
        return Ok(await _sender.Send(request));
    }
    
    [HttpDelete("{deviceId}/location/points")]
    [Authorize(Roles = $"{OwnConstants.Roles.SuperAdmin}")]
    public async Task<IActionResult> DeletePoints([FromHybrid] DeleteLocationPointsAdminRequest request)
    {
        await _sender.Send(request);
        return Ok();
    }
    
}