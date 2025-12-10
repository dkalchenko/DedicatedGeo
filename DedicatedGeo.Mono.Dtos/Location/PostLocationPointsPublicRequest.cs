using MediatR;

namespace DedicatedGeo.Mono.Dtos.Location;

public class PostLocationPointsPublicRequest: IRequest
{
    public List<LocationPointDto> LocationPoints { get; set; } = new();
}