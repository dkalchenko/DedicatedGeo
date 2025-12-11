using MediatR;

namespace DedicatedGeo.Mono.Dtos.Location;

public class GetLocationPointsAdminRequest: IRequest<GetLocationPointsAdminResponse>
{
    public string DeviceId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}