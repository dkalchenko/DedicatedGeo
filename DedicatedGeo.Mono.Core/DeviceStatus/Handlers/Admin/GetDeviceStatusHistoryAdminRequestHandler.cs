using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Abstractions.Common;
using DedicatedGeo.Mono.Core.Abstractions.Device.Services;
using DedicatedGeo.Mono.Core.Abstractions.DeviceAssignment;
using DedicatedGeo.Mono.Core.Abstractions.User.Services;
using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Entities;
using DedicatedGeo.Mono.Entities.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device;

public class GetDeviceStatusHistoryAdminRequestHandler: IRequestHandler<GetDeviceStatusHistoryAdminRequest, GetDeviceStatusHistoryAdminResponse>
{
    
    private readonly IDatabaseRepository _repository;
    private readonly IDeviceService _deviceService;
    private readonly IDeviceAssignmentService _deviceAssignmentService;
    private readonly IUsersServices _usersServices;

    public GetDeviceStatusHistoryAdminRequestHandler(IDatabaseRepository repository, IDeviceService deviceService, IDeviceAssignmentService deviceAssignmentService, IUsersServices usersServices)
    {
        _repository = repository.ThrowIfNull();
        _deviceService = deviceService.ThrowIfNull();
        _deviceAssignmentService = deviceAssignmentService.ThrowIfNull();
        _usersServices = usersServices.ThrowIfNull();
    }
    
    public async Task<GetDeviceStatusHistoryAdminResponse> Handle(GetDeviceStatusHistoryAdminRequest request, CancellationToken cancellationToken)
    {
        var deviceId = request.DeviceId.ToGuid();
        var device = await _deviceService.GetDeviceByIdAsync(deviceId, cancellationToken);

        if (device is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("device").GetException();
        }

        var user = await _usersServices.GetUserById(request.UserId.ToGuid(), cancellationToken);

        if (user is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceIsForbidden.FormatMessage("User is not found").GetException();
        }
        
        if (user.Role is OwnConstants.Roles.DeviceUser)
        {
            if (!await _deviceAssignmentService.IsDeviceAssignedToUserAsync(deviceId, request.UserId.ToGuid(), cancellationToken))
            {
                throw OwnConstants.ErrorTemplates.ResourceIsForbidden.GetException();
            } 
        }
        
        var result = await _repository.DeviceStatusHistories
            .Where(x => x.DeviceId == deviceId && x.StatusName == request.StatusName)
            .OrderByDescending(x => x.ChangedAt)
            .ThenByDescending(x => x.DeviceStatusHistoryId)
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(cancellationToken);
        
        
        return new GetDeviceStatusHistoryAdminResponse
        {
            StatusHistory = result.Select(x => new DeviceStatusHistoryItem
            {
                ChangedAt = x.ChangedAt,
                OldValue = x.OldValue,
                NewValue = x.NewValue
            })
        };
    }
}