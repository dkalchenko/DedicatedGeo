using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Abstractions.Device.Services;
using DedicatedGeo.Mono.Core.Abstractions.DeviceAssignment;
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
    private readonly IDeviceService _deviceService;
    private readonly IDeviceAssignmentService _deviceAssignmentService;

    public GetLocationPointsAdminRequestHandler(IDatabaseRepository repository, IDeviceService deviceService, IDeviceAssignmentService deviceAssignmentService)
    {
        _repository = repository.ThrowIfNull();
        _deviceService = deviceService.ThrowIfNull();
        _deviceAssignmentService = deviceAssignmentService.ThrowIfNull();
    }

    public async Task<GetLocationPointsAdminResponse> Handle(GetLocationPointsAdminRequest request, CancellationToken cancellationToken)
    {
        var deviceId = request.DeviceId.ToGuid();
        var device = await _deviceService.GetDeviceByIdAsync(deviceId, cancellationToken);

        if (device is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("device").GetException();
        }
        
        if (!await _deviceAssignmentService.IsDeviceAssignedToUserAsync(deviceId, request.UserId.ToGuid(), cancellationToken))
        {
            throw OwnConstants.ErrorTemplates.ResourceIsForbidden.GetException();
        }
        
        var to = request.To.ToUniversalTime();
        var from = request.From.ToUniversalTime();
        var points = await _repository.LocationPoints
            .Where(x => x.DeviceId == deviceId && x.CreatedAt >= from && x.CreatedAt <= to)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return new GetLocationPointsAdminResponse
        {
            Points = points.Select(x => x.ToLocationPointDto()).ToList()
        };
    }
}