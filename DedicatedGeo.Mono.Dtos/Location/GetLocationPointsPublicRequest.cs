using MediatR;

namespace DedicatedGeo.Mono.Dtos.Location;

public class GetLocationPointsPublicRequest: IRequest<GetLocationPointsPublicResponse>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}