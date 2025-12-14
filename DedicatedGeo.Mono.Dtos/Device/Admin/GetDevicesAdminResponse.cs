namespace DedicatedGeo.Mono.Dtos.Device;

public class GetDevicesAdminResponse
{
    public IEnumerable<GetDeviceAdminResponse> Devices { get; set; }
}