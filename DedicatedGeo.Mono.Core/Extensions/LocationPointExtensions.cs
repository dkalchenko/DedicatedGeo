using DedicatedGeo.Mono.Dtos;
using NetTopologySuite.Geometries;

namespace DedicatedGeo.Mono.Core.Extensions;

public static class LocationPointExtensions
{
    public static Point ToPoint(this LocationPointDto p)
    {
        var factory = new GeometryFactory(new PrecisionModel(), 4326); // SRID = 4326
        return factory.CreatePoint(new Coordinate(p.Longitude, p.Latitude)); // X = lon, Y = lat
    }
    public static LocationPointDto ToLocationPointDto(this Coordinate c) => new LocationPointDto
    {
        Latitude = c.Y,
        Longitude = c.X
    };
}