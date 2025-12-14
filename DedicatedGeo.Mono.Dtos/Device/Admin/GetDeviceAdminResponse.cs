namespace DedicatedGeo.Mono.Dtos.Device;

public class GetDeviceAdminResponse
{
    public Guid DeviceId { get; set; }
    public string IMEI { get; set; }
    public string DeviceName { get; set; }
}