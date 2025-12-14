using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Abstractions.Device.Services;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Entities.Device;
using DedicatedGeo.Mono.Entities.DeviceStatus;
using DedicatedGeo.Mono.Models.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device;

public class PutDeviceStatusS2DRequestHandler: IRequestHandler<PutDeviceStatusesS2DRequest>
{
    private readonly IDatabaseRepository _repository;
    private readonly IDeviceService _deviceService;
    private readonly IPublisher _publisher;

    public PutDeviceStatusS2DRequestHandler(IDatabaseRepository repository, IDeviceService deviceService, IPublisher publisher)
    {
        _repository = repository.ThrowIfNull();
        _deviceService = deviceService.ThrowIfNull();
        _publisher = publisher.ThrowIfNull();
    }
    
    public async Task Handle(PutDeviceStatusesS2DRequest request, CancellationToken cancellationToken)
    {
        var device = await _deviceService.GetDeviceByIMEIAsync(request.DeviceId, cancellationToken);

        if (device is null)
        {
            throw OwnConstants.ErrorTemplates
                .ResourceNotFound
                .FormatMessage("device")
                .GetException();
        }
        
        var deviceStatus = await _repository.DeviceStatuses.FirstOrDefaultAsync(x => x.DeviceId == device.DeviceId, cancellationToken: cancellationToken);

        Models.Device.DeviceStatus oldDeviceStatus = null;
        
        if (deviceStatus is null)
        {
            deviceStatus = new Models.Device.DeviceStatus
            {
                DeviceId = device.DeviceId,
                DeviceStatusId = Guid.NewGuid(),
                BatteryLevel = request.BatteryLevel,
                IsButtonPressed = request.IsButtonPressed,
                IsInAlarm = request.IsInAlarm,
                IsInCharge = request.IsInCharge,
                IsGPSOnline = request.IsGPSOnline,
                IsDeviceOnline = true,
                UpdatedAt = DateTime.UtcNow
            };
            _repository.DeviceStatuses.Add(deviceStatus);
        }
        else
        {
            oldDeviceStatus = new Models.Device.DeviceStatus
            {
                BatteryLevel = deviceStatus.BatteryLevel,
                IsButtonPressed = deviceStatus.IsButtonPressed,
                IsInAlarm = deviceStatus.IsInAlarm,
                IsInCharge = deviceStatus.IsInCharge,
                IsGPSOnline = deviceStatus.IsGPSOnline,
                IsDeviceOnline = deviceStatus.IsDeviceOnline,
                UpdatedAt = deviceStatus.UpdatedAt
            };
            deviceStatus.BatteryLevel = request.BatteryLevel;
            deviceStatus.IsButtonPressed = request.IsButtonPressed;
            deviceStatus.IsInAlarm = request.IsInAlarm;
            deviceStatus.IsInCharge = request.IsInCharge;
            deviceStatus.IsGPSOnline = request.IsGPSOnline;
            deviceStatus.IsDeviceOnline = true;
            deviceStatus.UpdatedAt = DateTime.UtcNow;    
        }
        
        

        await _repository.SaveChangesAsync(cancellationToken);

        await PublishDeviceStatusChangedEventIfStatusesChangedAsync(deviceStatus, oldDeviceStatus, cancellationToken);
    }
    
    private Task PublishDeviceStatusChangedEventIfStatusesChangedAsync(Models.Device.DeviceStatus newStatus, Models.Device.DeviceStatus? oldStatus, CancellationToken cancellationToken)
    {
        var changedStatuses = new List<ChangedDeviceStatusItem>();
        if (newStatus.IsButtonPressed != oldStatus?.IsButtonPressed)
        {
            changedStatuses.Add(new ChangedDeviceStatusItem
            {
                NewValue =  newStatus.IsButtonPressed.ToString(),
                OldValue =  oldStatus?.IsButtonPressed.ToString(),
                StatusName = OwnConstants.DeviceStatusNames.IsButtonPressed,
                DeviceId =  newStatus.DeviceId,
                ChangedAt =  newStatus.UpdatedAt,
            });
        }
        if (newStatus.BatteryLevel != oldStatus?.BatteryLevel)
        {
            changedStatuses.Add(new ChangedDeviceStatusItem
            {
                NewValue =  newStatus.BatteryLevel.ToString(),
                OldValue =  oldStatus?.BatteryLevel.ToString(),
                StatusName = OwnConstants.DeviceStatusNames.BatteryLevel,
                DeviceId =  newStatus.DeviceId,
                ChangedAt =  newStatus.UpdatedAt,
            });
        }
        if (newStatus.IsInAlarm != oldStatus?.IsInAlarm)
        {
            changedStatuses.Add(new ChangedDeviceStatusItem
            {
                NewValue =  newStatus.IsInAlarm.ToString(),
                OldValue =  oldStatus?.IsInAlarm.ToString(),
                StatusName = OwnConstants.DeviceStatusNames.IsInAlarm,
                DeviceId =  newStatus.DeviceId,
                ChangedAt =  newStatus.UpdatedAt,
            });
        }
        if (newStatus.IsDeviceOnline != oldStatus?.IsDeviceOnline)
        {
            changedStatuses.Add(new ChangedDeviceStatusItem
            {
                NewValue =  newStatus.IsDeviceOnline.ToString(),
                OldValue =  oldStatus?.IsDeviceOnline.ToString(),
                StatusName = OwnConstants.DeviceStatusNames.IsDeviceOnline,
                DeviceId =  newStatus.DeviceId,
                ChangedAt =  newStatus.UpdatedAt,
            });
        }
        if (newStatus.IsInCharge != oldStatus?.IsInCharge)
        {
            changedStatuses.Add(new ChangedDeviceStatusItem
            {
                NewValue =  newStatus.IsInCharge.ToString(),
                OldValue =  oldStatus?.IsInCharge.ToString(),
                StatusName = OwnConstants.DeviceStatusNames.IsInCharge,
                DeviceId =  newStatus.DeviceId,
                ChangedAt =  newStatus.UpdatedAt,
            });
        }
        if (newStatus.IsGPSOnline != oldStatus?.IsGPSOnline)
        {
            changedStatuses.Add(new ChangedDeviceStatusItem
            {
                NewValue =  newStatus.IsGPSOnline.ToString(),
                OldValue =  oldStatus?.IsGPSOnline.ToString(),
                StatusName = OwnConstants.DeviceStatusNames.IsGPSOnline,
                DeviceId =  newStatus.DeviceId,
                ChangedAt =  newStatus.UpdatedAt,
            });
        }
        
        return _publisher.Publish(new DeviceStatusesChangedNotification
        {
            ChangedDeviceStatuses = changedStatuses
        }, cancellationToken);
    }
}