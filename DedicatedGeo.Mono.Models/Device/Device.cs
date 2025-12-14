using DedicatedGeo.Mono.Models.Location;

namespace DedicatedGeo.Mono.Models.Device;

public class Device
{
    public Guid DeviceId { get; set; }
    public string IMEI { get; set; }
    public string Name { get; set; }
    public virtual DeviceStatus DeviceStatus { get; set; }
    public virtual List<DeviceStatusHistory> DeviceStatusHistories { get; set; }
    public virtual List<DeviceAssignment> DeviceAssignments { get; set; }
    public virtual List<LocationPoint> LocationPoints { get; set; }
}