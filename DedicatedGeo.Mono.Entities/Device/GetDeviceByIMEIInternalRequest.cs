using MediatR;

namespace DedicatedGeo.Mono.Entities.Device;

public class GetDeviceByIMEIInternalRequest: IRequest<GetDeviceInternalResponse>
{
    public string DeviceId { get; set; }
}