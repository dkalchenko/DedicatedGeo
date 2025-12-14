using DedicatedGeo.Mono.Entities.Device;

namespace DedicatedGeo.Mono.Core.Abstractions.DeviceAssignment;

public interface IDeviceAssignmentService
{
    public Task<bool> IsDeviceAssignedToUserAsync(Guid deviceId, Guid userId, CancellationToken cancellationToken);

    public Task<IEnumerable<Guid>> GetDevicesAssignedToUserAsync(Guid userId, int offset, int limit,
        CancellationToken cancellationToken);
}