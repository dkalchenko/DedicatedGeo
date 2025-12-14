using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class DeleteDeviceAdminRequest: IRequest
{
    public Guid DeviceId { get; set; }
}