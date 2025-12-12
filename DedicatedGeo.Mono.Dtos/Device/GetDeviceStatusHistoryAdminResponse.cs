namespace DedicatedGeo.Mono.Dtos.Device;

public class GetDeviceStatusHistoryAdminResponse
{
    public IEnumerable<DeviceStatusHistoryItem> StatusHistory { get; set; }
}

public class DeviceStatusHistoryItem
{
    public DateTime ChangedAt { get; set; }
    public string? OldValue { get; set; }
    public string NewValue { get; set; }
}