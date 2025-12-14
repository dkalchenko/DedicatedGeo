using DedicatedGeo.Mono.Common;
using HybridModelBinding;
using MediatR;

namespace DedicatedGeo.Mono.Dtos.Location;

public class GetLocationPointsAdminRequest: IRequest<GetLocationPointsAdminResponse>
{
    [HybridBindProperty(Source.Route, OwnConstants.Claims.UserIdClaim)]
    public string UserId { get; set; }
    public string DeviceId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}