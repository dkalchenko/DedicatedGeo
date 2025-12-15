using DedicatedGeo.Mono.Common;
using HybridModelBinding;
using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class GetDevicesAdminRequest: IRequest<GetDevicesAdminResponse>
{
    [HybridBindProperty(Source.Claim, OwnConstants.Claims.UserIdClaim)]
    public string UserId { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; } = 10;
}