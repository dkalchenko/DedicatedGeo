using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Abstractions.DeviceAssignment;
using DedicatedGeo.Mono.Dal.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.DeviceAssignment.Services;

public class DeviceAssignmentService: IDeviceAssignmentService
{
    
    private IDatabaseRepository _repository;
    
    public DeviceAssignmentService(IDatabaseRepository databaseRepository)
    {
        _repository = databaseRepository.ThrowIfNull();
    }
    
    public async Task<bool> IsDeviceAssignedToUserAsync(Guid deviceId, Guid userId, CancellationToken cancellationToken)
    {
        var deviceAssignment = await _repository.DeviceAssignments.FirstOrDefaultAsync(x => x.DeviceId == deviceId && x.UserId == userId, cancellationToken);

        return deviceAssignment is not null;
    }
    
    public async Task<IEnumerable<Guid>> GetDevicesAssignedToUserAsync(Guid userId, int offset, int limit, CancellationToken cancellationToken)
    {
        var deviceAssignments = await _repository.DeviceAssignments.Where(x => x.UserId == userId)
            .Skip(offset).Take(limit).ToListAsync(cancellationToken);

        return deviceAssignments.Select(x => x.DeviceId);
    }
}