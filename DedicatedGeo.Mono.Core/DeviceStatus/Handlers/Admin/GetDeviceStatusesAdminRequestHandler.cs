using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Abstractions.Device.Services;
using DedicatedGeo.Mono.Core.Abstractions.DeviceAssignment;
using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Entities.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device;

public class GetDeviceStatusesAdminRequestHandler: IRequestHandler<GetDeviceStatusesAdminRequest, GetDeviceStatusesAdminResponse>
{
    
    private readonly IDatabaseRepository _repository;
    private readonly IDeviceService _deviceService;
    private readonly IDeviceAssignmentService _deviceAssignmentService;

    public GetDeviceStatusesAdminRequestHandler(IDatabaseRepository repository, IDeviceService deviceService, IDeviceAssignmentService deviceAssignmentService)
    {
        _repository = repository.ThrowIfNull();
        _deviceService = deviceService.ThrowIfNull();
        _deviceAssignmentService = deviceAssignmentService.ThrowIfNull();
    }
    
    public async Task<GetDeviceStatusesAdminResponse> Handle(GetDeviceStatusesAdminRequest request, CancellationToken cancellationToken)
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
        
        var deviceStatus = await _repository.DeviceStatuses
            .AsQueryable()
            .Where(x => x.DeviceId == deviceId)
            .OrderByDescending(x => x.UpdatedAt)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (deviceStatus is null)
        {
            return new GetDeviceStatusesAdminResponse();
        }
        
        return new GetDeviceStatusesAdminResponse
        {
            IsGPSOnline = deviceStatus.IsGPSOnline,
            BatteryLevel = deviceStatus.BatteryLevel,
            IsButtonPressed = deviceStatus.IsButtonPressed,
            IsInAlarm = deviceStatus.IsInAlarm,
            IsInCharge = deviceStatus.IsInCharge,
            IsDeviceOnline = deviceStatus.IsDeviceOnline,
            UpdatedAt = deviceStatus.UpdatedAt,
        };
    }
}