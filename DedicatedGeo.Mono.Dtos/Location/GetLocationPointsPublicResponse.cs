namespace DedicatedGeo.Mono.Dtos.Location;

public class GetLocationPointsPublicResponse
{
    public IEnumerable<LocationPointDto> Points { get; set; } = null!;
}