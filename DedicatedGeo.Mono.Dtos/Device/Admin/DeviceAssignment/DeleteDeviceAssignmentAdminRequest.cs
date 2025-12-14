using MediatR;

namespace DedicatedGeo.Mono.Dtos.Device.Admin.DeviceAssignment;

public class DeleteDeviceAssignmentAdminRequest: IRequest
{
    public string DeviceAssignmentId { get; set; }
}