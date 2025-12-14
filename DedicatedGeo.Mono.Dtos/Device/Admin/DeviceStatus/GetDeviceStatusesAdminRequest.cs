using DedicatedGeo.Mono.Common;
using HybridModelBinding;
using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class GetDeviceStatusesAdminRequest: IRequest<GetDeviceStatusesAdminResponse>
{
    public string DeviceId { get; set; }
    [HybridBindProperty(Source.Route, OwnConstants.Claims.UserIdClaim)]
    public string UserId { get; set; }
}