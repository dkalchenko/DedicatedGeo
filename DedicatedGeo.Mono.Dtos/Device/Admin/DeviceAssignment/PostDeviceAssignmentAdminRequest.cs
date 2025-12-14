using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device.Admin.DeviceAssignment;

public class PostDeviceAssignmentAdminRequest: IRequest<DeviceAssignmentAdminResponse>
{
    public string UserId { get; set; }
    public string DeviceId { get; set; }
}