using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Abstractions.DeviceAssignment;
using DedicatedGeo.Mono.Core.Abstractions.User.Services;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Dtos.Device.Admin;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device.Handlers.Admin;

public class GetDeviceAdminRequestHandler: IRequestHandler<GetDeviceAdminRequest, GetDeviceAdminResponse>
{
    private readonly IDatabaseRepository _databaseRepository;
    private readonly IDeviceAssignmentService _deviceAssignmentService;
    private readonly IUsersServices _usersServices;
    
    public GetDeviceAdminRequestHandler(IDatabaseRepository databaseRepository, IDeviceAssignmentService deviceAssignmentService, IUsersServices usersServices)
    {
        _databaseRepository = databaseRepository.ThrowIfNull();
        _deviceAssignmentService = deviceAssignmentService.ThrowIfNull();
        _usersServices = usersServices.ThrowIfNull();
    }
    
    public async Task<GetDeviceAdminResponse> Handle(GetDeviceAdminRequest request, CancellationToken cancellationToken)
    {
        var device = await _databaseRepository.Devices
            .FirstOrDefaultAsync(x => x.DeviceId == request.DeviceId.ToGuid(), cancellationToken: cancellationToken);
        
        if (device is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("device").GetException();
        }
        
        var user = await _usersServices.GetUserById(request.UserId.ToGuid(), cancellationToken);

        if (user is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceIsForbidden.FormatMessage("User is not found").GetException();
        }

        if (user.Role is OwnConstants.Roles.DeviceUser && !await _deviceAssignmentService.IsDeviceAssignedToUserAsync(device.DeviceId, request.UserId.ToGuid(), cancellationToken))
        {
            throw OwnConstants.ErrorTemplates.ResourceIsForbidden.GetException();
        }

        return new GetDeviceAdminResponse
        {
            DeviceId = device.DeviceId,
            IMEI = device.IMEI,
            DeviceName = device.Name
        };
    }
}