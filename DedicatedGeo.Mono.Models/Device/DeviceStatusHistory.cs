namespace DedicatedGeo.Mono.Models.Device;

public class DeviceStatusHistory
{
    public Guid DeviceStatusHistoryId { get; set; }
    public Guid DeviceId { get; set; }
    public string StatusName { get; set; }
    public string? OldValue { get; set; }
    public string NewValue { get; set; }
    public DateTime ChangedAt { get; set; }
}