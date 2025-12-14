using DedicatedGeo.Mono.Common;
using HybridModelBinding;
using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class PostDeviceAdminRequest: IRequest<GetDeviceAdminResponse>
{
    [HybridBindProperty(Source.Route, OwnConstants.Claims.UserIdClaim)]
    public string UserId { get; set; }
    public string IMEI { get; set; }
    public string DeviceName { get; set; }
}