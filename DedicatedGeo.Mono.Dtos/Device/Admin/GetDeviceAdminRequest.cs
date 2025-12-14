using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class GetDeviceAdminRequest: IRequest<GetDeviceAdminResponse>
{
    public Guid DeviceId { get; set; }
}