using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Location;
using DedicatedGeo.Mono.Entities.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Location;

public class GetLocationPointsAdminRequestHandler: IRequestHandler<GetLocationPointsAdminRequest, GetLocationPointsAdminResponse>
{
    private readonly IDatabaseRepository _repository;
    private readonly ISender _sender;

    public GetLocationPointsAdminRequestHandler(IDatabaseRepository repository, ISender sender)
    {
        _repository = repository.ThrowIfNull();
        _sender = sender.ThrowIfNull();
    }
    
    public async Task<GetLocationPointsAdminResponse> Handle(GetLocationPointsAdminRequest request, CancellationToken cancellationToken)
    {
        var device = await _sender.Send(new GetDeviceInternalRequest
        {
            DeviceId = request.DeviceId
        }, cancellationToken);

        if (device is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("device").GetException();
        }
        
        var to = request.To.ToUniversalTime();
        var from = request.From.ToUniversalTime();
        var points = await _repository.LocationPoints
            .Where(x => x.CreatedAt >= from && x.CreatedAt <= to && x.DeviceId == request.DeviceId.ToGuid())
            .ToListAsync(cancellationToken: cancellationToken);
        
        return new GetLocationPointsAdminResponse
        {
            Points = points.Select(x => x.ToLocationPointDto()).ToList()
        };
    }
}