using NetTopologySuite.Geometries;

namespace DedicatedGeo.Mono.Models.Location;

public class LocationPoint
{
    public Guid LocationPointId { get; set; }
    public Point Point { get; set; }
    public DateTime CreatedAt { get; set; }
}