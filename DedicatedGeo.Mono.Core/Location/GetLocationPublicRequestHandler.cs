using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Location;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Location;

public class GetLocationPublicRequestHandler: IRequestHandler<GetLocationPointsPublicRequest, GetLocationPointsPublicResponse>
{
    private readonly IDatabaseRepository _repository;

    public GetLocationPublicRequestHandler(IDatabaseRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }
    
    public async Task<GetLocationPointsPublicResponse> Handle(GetLocationPointsPublicRequest request, CancellationToken cancellationToken)
    {
        var to = request.To;
        var from = request.From;
        var points = await _repository.LocationPoints
            .Where(x => x.CreatedAt >= from && x.CreatedAt <= to)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return new GetLocationPointsPublicResponse
        {
            Points = points.Select(x => x.ToLocationPointDto()).ToList()
        };
    }
}