namespace DedicatedGeo.Mono.Models.Device;

public class DeviceAssignment
{
    public Guid DeviceAssignmentId { get; set; }
    public Guid DeviceId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
}