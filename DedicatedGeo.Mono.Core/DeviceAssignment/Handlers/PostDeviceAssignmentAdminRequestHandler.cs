using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Abstractions.Device.Services;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device.Admin.DeviceAssignment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.DeviceAssignment.Handlers;

public class PostDeviceAssignmentAdminRequestHandler: IRequestHandler<PostDeviceAssignmentAdminRequest, DeviceAssignmentAdminResponse>
{
    private readonly IDatabaseRepository _repository;
    private readonly IDeviceService _deviceService;

    public PostDeviceAssignmentAdminRequestHandler(IDatabaseRepository repository, IDeviceService deviceService)
    {
        _repository = repository.ThrowIfNull();
        _deviceService = deviceService.ThrowIfNull();
    }
    
    public async Task<DeviceAssignmentAdminResponse> Handle(PostDeviceAssignmentAdminRequest request, CancellationToken cancellationToken)
    {
        var deviceId = request.DeviceId.ToGuid();
        var device = await _deviceService.GetDeviceByIdAsync(deviceId, cancellationToken);
        
        if (device is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("device").GetException();
        }

        var userId = request.UserId.ToGuid();
        
        var existedAssignment = await _repository.DeviceAssignments.FirstOrDefaultAsync(x => x.DeviceId == deviceId && x.UserId == userId, cancellationToken);
        
        if (existedAssignment is not null)
        {
            throw OwnConstants.ErrorTemplates.ResourceAlreadyExists.FormatMessage("device assignment").GetException();
        }

        var newAssignment = new Models.Device.DeviceAssignment
        {
            DeviceId = deviceId,
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            DeviceAssignmentId = Guid.NewGuid()
        };
        
        _repository.DeviceAssignments.Add(newAssignment);
        await _repository.SaveChangesAsync(cancellationToken);

        return new DeviceAssignmentAdminResponse
        {
            DeviceId =  deviceId,
            UserId =  userId,
            DeviceAssignmentId = newAssignment.DeviceAssignmentId
        };
    }
}