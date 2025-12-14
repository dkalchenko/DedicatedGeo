using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Abstractions.Device.Services;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device.Admin.DeviceAssignment;
using DedicatedGeo.Mono.Dtos.Device.DeviceAssignment;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.DeviceAssignment.Handlers;

public class GetDeviceAssignmentsAdminRequestHandler: IRequestHandler<GetDeviceAssignmentsAdminRequest, GetDeviceAssignmentsAdminResponse>
{
    private readonly IDatabaseRepository _repository;
    private readonly IDeviceService _deviceService;

    public GetDeviceAssignmentsAdminRequestHandler(IDatabaseRepository repository, IDeviceService deviceService)
    {
        _repository = repository.ThrowIfNull();
        _deviceService = deviceService.ThrowIfNull();
    }
    
    public async Task<GetDeviceAssignmentsAdminResponse> Handle(GetDeviceAssignmentsAdminRequest request, CancellationToken cancellationToken)
    {
        var deviceId = request.DeviceId.ToGuid();
        var device = await _deviceService.GetDeviceByIdAsync(deviceId, cancellationToken);
        
        if (device is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("device").GetException();
        }
        
        var assignments = await _repository.DeviceAssignments
            .Where(x => x.DeviceId == deviceId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);

        return new GetDeviceAssignmentsAdminResponse
        {
            DeviceAssignments = assignments.Select(x => new DeviceAssignmentAdminResponse
            {
                DeviceId = x.DeviceId,
                DeviceAssignmentId = x.DeviceAssignmentId,
                UserId = x.UserId,
            })
        };
    }
}