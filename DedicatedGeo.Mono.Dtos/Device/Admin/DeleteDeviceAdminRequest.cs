using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class DeleteDeviceAdminRequest: IRequest
{
    public string DeviceId { get; set; }
}