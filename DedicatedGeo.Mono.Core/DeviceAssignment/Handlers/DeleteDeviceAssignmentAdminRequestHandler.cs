using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device.Admin.DeviceAssignment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.DeviceAssignment.Handlers;

public class DeleteDeviceAssignmentAdminRequestHandler: IRequestHandler<DeleteDeviceAssignmentAdminRequest>
{
    
    private readonly IDatabaseRepository _repository;

    public DeleteDeviceAssignmentAdminRequestHandler(IDatabaseRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }
    
    public async Task Handle(DeleteDeviceAssignmentAdminRequest request, CancellationToken cancellationToken)
    {
        var deviceAssignment = await _repository.DeviceAssignments
            .FirstOrDefaultAsync(x => x.DeviceAssignmentId == request.DeviceAssignmentId.ToGuid(), cancellationToken: cancellationToken);

        if (deviceAssignment is null)
        {
            return;
        }
        
        _repository.DeviceAssignments.Remove(deviceAssignment);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}