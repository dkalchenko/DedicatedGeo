using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Entities.DeviceStatus;
using DedicatedGeo.Mono.Models.Device;
using MediatR;

namespace DedicatedGeo.Mono.Core.DeviceStatus.NotificationHandlers;

public class DeviceStatusChangedNotificationHandler: INotificationHandler<DeviceStatusesChangedNotification>
{
    private readonly IDatabaseRepository _repository;

    public DeviceStatusChangedNotificationHandler(IDatabaseRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }

    
    public async Task Handle(DeviceStatusesChangedNotification notification, CancellationToken cancellationToken)
    {
        foreach (var changedDeviceStatus in notification.ChangedDeviceStatuses)
        {
            _repository.DeviceStatusHistories.Add(
                new DeviceStatusHistory
                {
                    DeviceId = changedDeviceStatus.DeviceId,
                    NewValue = changedDeviceStatus.NewValue,
                    OldValue = changedDeviceStatus.OldValue,
                    StatusName = changedDeviceStatus.StatusName,
                    ChangedAt = DateTime.UtcNow,
                    DeviceStatusHistoryId = Guid.NewGuid()
                });   
        }

        await _repository.SaveChangesAsync(cancellationToken);
    }
}