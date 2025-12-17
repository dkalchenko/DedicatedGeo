using DedicatedGeo.Mono.Common;
using HybridModelBinding;
using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device.Admin;

public class GetDeviceAdminRequest: IRequest<GetDeviceAdminResponse>
{
    [HybridBindProperty(Source.Claim, OwnConstants.Claims.UserIdClaim)]
    public string UserId { get; set; }
    public string DeviceId { get; set; }
}