using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class PutDeviceAdminRequest: IRequest<GetDeviceAdminResponse>
{
    public Guid DeviceId { get; set; }
    public string DeviceName { get; set; }
}