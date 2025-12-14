namespace DedicatedGeo.Mono.Dtos.Device;

public class GetDeviceStatusesAdminResponse
{
    public int BatteryLevel { get; set; }
    public bool IsDeviceOnline { get; set; }
    public bool IsInAlarm { get; set; }
    public bool IsButtonPressed { get; set; }
    public bool IsInCharge { get; set; }
    public bool IsGPSOnline { get; set; }
    public DateTime UpdatedAt { get; set; }
}