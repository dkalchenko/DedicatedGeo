using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Abstractions.Device.Services;
using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Location;
using DedicatedGeo.Mono.Entities.Device;
using DedicatedGeo.Mono.Models.Location;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace DedicatedGeo.Mono.Core.Location;

public class PostLocationPointsS2DRequestHandler: IRequestHandler<PostLocationPointsS2DRequest>
{

    private readonly IDatabaseRepository _repository;
    private readonly IDeviceService _deviceService;

    public PostLocationPointsS2DRequestHandler(IDatabaseRepository repository, IDeviceService deviceService)
    {
        _repository = repository.ThrowIfNull();
        _deviceService = deviceService.ThrowIfNull();
    }
    
    public async Task Handle(PostLocationPointsS2DRequest request, CancellationToken cancellationToken)
    {
        var device = await _deviceService.GetDeviceByIMEIAsync(request.DeviceId, cancellationToken);

        if (device is null)
        {
            throw OwnConstants.ErrorTemplates
                .ResourceNotFound
                .FormatMessage("device")
                .GetException();
        }
        
        var locationPoints = request.LocationPoints.Select(
            locationPoint => new LocationPoint
            {
                DeviceId = device.DeviceId,
                Point = locationPoint.ToPoint(),
                CreatedAt = DateTimeOffset.FromUnixTimeSeconds(locationPoint.CreatedAt).UtcDateTime
            }
        ).ToList();
        
        _repository.LocationPoints.AddRange(locationPoints);
        
        await _repository.SaveChangesAsync(cancellationToken);
    }
}