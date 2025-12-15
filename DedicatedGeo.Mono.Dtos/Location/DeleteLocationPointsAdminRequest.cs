using DedicatedGeo.Mono.Common;
using HybridModelBinding;
using MediatR;

namespace DedicatedGeo.Mono.Dtos.Location;

public class DeleteLocationPointsAdminRequest: IRequest
{
    
    [HybridBindProperty(Source.Claim, OwnConstants.Claims.UserIdClaim)]
    public string UserId { get; set; }
    public string DeviceId { get; set; }
    public string LocationPointIds { get; set; }
}