namespace DedicatedGeo.Mono.Dtos.Location;

public class GetLocationPointsAdminResponse
{
    public IEnumerable<LocationPointDto> Points { get; set; } = null!;
}