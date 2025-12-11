using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class GetDevicesAdminRequest: IRequest<GetDevicesAdminResponse>
{
    public int Offset { get; set; }
    public int Limit { get; set; } = 10;
}