using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class GetDeviceStatusAdminRequest: IRequest<GetDeviceStatusAdminResponse>
{
    public string DeviceId { get; set; }
}