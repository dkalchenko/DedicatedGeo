namespace DedicatedGeo.Mono.Dtos.Device.Admin.DeviceAssignment;

public class GetDeviceAssignmentsAdminResponse
{
    public IEnumerable<DeviceAssignmentAdminResponse> DeviceAssignments { get; set; }
}