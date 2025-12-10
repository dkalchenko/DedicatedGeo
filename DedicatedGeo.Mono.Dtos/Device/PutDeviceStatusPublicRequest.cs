using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class PutDeviceStatusPublicRequest: IRequest
{
    public int BatteryLevel { get; set; }
    public bool IsInAlarm { get; set; }
    public bool IsButtonPressed { get; set; }
    public bool IsInCharge { get; set; }
    public bool IsGPSOnline { get; set; }
}