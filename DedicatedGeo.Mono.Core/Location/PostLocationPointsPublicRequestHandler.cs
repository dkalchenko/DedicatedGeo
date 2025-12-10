using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Location;
using DedicatedGeo.Mono.Models.Location;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace DedicatedGeo.Mono.Core.Location;

public class PostLocationPointsPublicRequestHandler: IRequestHandler<PostLocationPointsPublicRequest>
{

    private readonly IDatabaseRepository _repository;

    public PostLocationPointsPublicRequestHandler(IDatabaseRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }
    
    public async Task Handle(PostLocationPointsPublicRequest request, CancellationToken cancellationToken)
    {
        var locationPoints = request.LocationPoints.Select(
            locationPoint => new LocationPoint
            {
                Point = locationPoint.ToPoint(),
                CreatedAt = DateTime.UtcNow
            }
            ).ToList();
        _repository.LocationPoints.AddRange(locationPoints);
        try
        {
            await _repository.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            // Include inner exception details to diagnose the real cause
            var inner = ex.InnerException?.ToString() ?? "No inner exception";
            throw new InvalidOperationException($"SaveChanges failed: {ex.Message}. Inner: {inner}", ex);
        }
    }
}