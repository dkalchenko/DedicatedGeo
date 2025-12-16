using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Abstractions.DeviceAssignment;
using DedicatedGeo.Mono.Core.Abstractions.User.Services;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device.Handlers.Admin;

public class GetDevicesAdminRequestHandler: IRequestHandler<GetDevicesAdminRequest, GetDevicesAdminResponse>
{
    private readonly IDatabaseRepository _databaseRepository;
    private readonly IDeviceAssignmentService _deviceAssignmentService;
    private readonly IUsersServices _usersServices;
    public GetDevicesAdminRequestHandler(IDatabaseRepository databaseRepository, IDeviceAssignmentService deviceAssignmentService, IUsersServices usersServices)
    {
        _databaseRepository = databaseRepository.ThrowIfNull();
        _deviceAssignmentService = deviceAssignmentService.ThrowIfNull();
        _usersServices = usersServices.ThrowIfNull();
    }
    
    public async Task<GetDevicesAdminResponse> Handle(GetDevicesAdminRequest request, CancellationToken cancellationToken)
    {
        var user = await _usersServices.GetUserById(request.UserId.ToGuid(), cancellationToken);

        if (user is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceIsForbidden.FormatMessage("User is not found").GetException();
        }

        IQueryable<Models.Device.Device> query;
        
        if (user.Role is OwnConstants.Roles.DeviceUser)
        {
            var deviceIds = await _deviceAssignmentService.GetDevicesAssignedToUserAsync(
                request.UserId.ToGuid(), request.Offset, request.Limit, cancellationToken);

            query = _databaseRepository.Devices.AsQueryable()
                .Where(x => deviceIds.Contains(x.DeviceId));    
        }
        else
        {
            query = _databaseRepository.Devices.AsQueryable();
        }

        if (request.Search is not null)
        {
            query = query.Where(x => x.Name.Contains(request.Search) || x.IMEI.Contains(request.Search));
        }

        var devices = await query.Skip(request.Offset).Take(request.Limit).ToListAsync(cancellationToken: cancellationToken);

        return new GetDevicesAdminResponse
        {
            Devices = devices.Select(x => new GetDeviceAdminResponse
            {
                DeviceId = x.DeviceId,
                IMEI = x.IMEI,
                DeviceName = x.Name,
            })
        };
    }
}