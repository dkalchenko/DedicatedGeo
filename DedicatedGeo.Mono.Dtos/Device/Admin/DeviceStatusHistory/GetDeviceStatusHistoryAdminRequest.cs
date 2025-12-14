using DedicatedGeo.Mono.Common;
using HybridModelBinding;
using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class GetDeviceStatusHistoryAdminRequest: IRequest<GetDeviceStatusHistoryAdminResponse>
{
    
    [HybridBindProperty(Source.Route, OwnConstants.Claims.UserIdClaim)]
    public string UserId { get; set; }
    public string DeviceId { get; set; }
    public string StatusName { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; } = 50;
}