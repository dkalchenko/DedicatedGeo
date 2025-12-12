using MediatR;

namespace DedicatedGeo.Mono.Entities.DeviceStatus;

public class DeviceStatusesChangedNotification: INotification
{
    public IEnumerable<ChangedDeviceStatusItem> ChangedDeviceStatuses { get; set; }
}

public class ChangedDeviceStatusItem
{
    public required string StatusName { get; set; }
    public string? OldValue { get; set; }
    public required string NewValue { get; set; }
    public Guid DeviceId { get; set; }
    public DateTime ChangedAt { get; set; }
}