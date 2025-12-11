using MediatR;

namespace DedicatedGeo.Mono.Entities.Device;

public class GetDeviceInternalRequest: IRequest<GetDeviceInternalResponse>
{
    public string DeviceId { get; set; }
}