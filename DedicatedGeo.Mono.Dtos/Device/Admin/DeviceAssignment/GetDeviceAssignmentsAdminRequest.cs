using DedicatedGeo.Mono.Dtos.Device.Admin.DeviceAssignment;
using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device.DeviceAssignment;

public class GetDeviceAssignmentsAdminRequest: IRequest<GetDeviceAssignmentsAdminResponse>
{
    public string DeviceId { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
}