using MediatR;

namespace DedicatedGeo.Mono.Dtos.Location;

public class DeleteLocationPointsAdminRequest: IRequest
{
    public string DeviceId { get; set; }
    public string LocationPointIds { get; set; }
}