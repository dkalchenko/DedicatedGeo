using MediatR;

namespace DedicatedGeo.Mono.Dtos.Location;

public class PostLocationPointsPublicRequest: IRequest
{
    public List<PostLocationPointDto> LocationPoints { get; set; } = new();
}

public class PostLocationPointDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public long CreatedAt { get; set; }
}