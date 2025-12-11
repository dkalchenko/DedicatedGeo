using DedicatedGeo.Mono.Dtos;
using DedicatedGeo.Mono.Dtos.Location;
using DedicatedGeo.Mono.Models.Location;
using NetTopologySuite.Geometries;

namespace DedicatedGeo.Mono.Core.Extensions;

public static class LocationPointExtensions
{
    public static Point ToPoint(this PostLocationPointDto p)
    {
        var factory = new GeometryFactory(new PrecisionModel(), 4326); // SRID = 4326
        return factory.CreatePoint(new Coordinate(p.Longitude, p.Latitude)); // X = lon, Y = lat
    }
    public static LocationPointDto ToLocationPointDto(this LocationPoint c) => new LocationPointDto
    {
        LocationPointId = c.LocationPointId,
        Latitude = c.Point.Y,
        Longitude = c.Point.X,
        CreatedAt = c.CreatedAt
    };
}