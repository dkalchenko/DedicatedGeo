using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device;

public class GetDeviceStatusHistoryAdminRequest: IRequest<GetDeviceStatusHistoryAdminResponse>
{
    public string DeviceId { get; set; }
    public string StatusName { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; } = 50;
}