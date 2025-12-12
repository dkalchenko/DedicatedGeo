namespace DedicatedGeo.Mono.Models.Device;

public class DeviceStatus
{
    public Guid DeviceId { get; set; }
    public Guid DeviceStatusId { get; set; }
    public int BatteryLevel { get; set; }
    public bool IsInAlarm { get; set; }
    public bool IsButtonPressed { get; set; }
    public bool IsInCharge { get; set; }
    public bool IsGPSOnline { get; set; }
    public bool IsDeviceOnline { get; set; }
    public DateTime UpdatedAt { get; set; }
}