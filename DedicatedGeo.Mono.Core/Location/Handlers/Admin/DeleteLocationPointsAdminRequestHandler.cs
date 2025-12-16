using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Abstractions.Device.Services;
using DedicatedGeo.Mono.Core.Abstractions.DeviceAssignment;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Location;
using DedicatedGeo.Mono.Entities.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Location;

public class DeleteLocationPointsAdminRequestHandler: IRequestHandler<DeleteLocationPointsAdminRequest>
{
    private readonly IDatabaseRepository _repository;
    private readonly IDeviceService _deviceService;

    public DeleteLocationPointsAdminRequestHandler(IDatabaseRepository repository, IDeviceService deviceService)
    {
        _repository = repository.ThrowIfNull();
        _deviceService = deviceService.ThrowIfNull();
    }
    
    public async Task Handle(DeleteLocationPointsAdminRequest request, CancellationToken cancellationToken)
    {
        var deviceId = request.DeviceId.ToGuid();
        var device = await _deviceService.GetDeviceByIdAsync(deviceId, cancellationToken);

        if (device is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("device").GetException();
        }
        
        var locationPointIds = request.LocationPointIds?.Split(',').Select(Guid.Parse).ToList();
        
        var query = _repository.LocationPoints.Where(x => x.DeviceId == deviceId);

        if (locationPointIds is not null)
        {
            query = query.Where(x => locationPointIds.Contains(x.LocationPointId));
        }
        
        var points = await query.ToListAsync(cancellationToken);
        
        if (points.Count > 0)
        {
            _repository.LocationPoints.RemoveRange(points);
            await _repository.SaveChangesAsync(cancellationToken);
        }

    }
}