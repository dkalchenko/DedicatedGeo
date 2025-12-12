using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class GetDeviceStatusesAdminRequest: IRequest<GetDeviceStatusesAdminResponse>
{
    public string DeviceId { get; set; }
}