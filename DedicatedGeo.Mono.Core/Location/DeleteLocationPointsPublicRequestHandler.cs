using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Location;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Location;

public class DeleteLocationPointsPublicRequestHandler: IRequestHandler<DeleteLocationPointsPublicRequest>
{
    private readonly IDatabaseRepository _repository;

    public DeleteLocationPointsPublicRequestHandler(IDatabaseRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }

    
    public async Task Handle(DeleteLocationPointsPublicRequest request, CancellationToken cancellationToken)
    {
        var points = await _repository.LocationPoints.ToListAsync(cancellationToken);
        if (points.Count > 0)
        {
            _repository.LocationPoints.RemoveRange(points);
            await _repository.SaveChangesAsync(cancellationToken);
        }

    }
}