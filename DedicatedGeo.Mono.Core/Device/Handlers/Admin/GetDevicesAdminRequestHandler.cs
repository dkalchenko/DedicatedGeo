using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Abstractions.DeviceAssignment;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device.Handlers.Admin;

public class GetDevicesAdminRequestHandler: IRequestHandler<GetDevicesAdminRequest, GetDevicesAdminResponse>
{
    private readonly IDatabaseRepository _databaseRepository;
    private readonly IDeviceAssignmentService _deviceAssignmentService;
    public GetDevicesAdminRequestHandler(IDatabaseRepository databaseRepository, IDeviceAssignmentService deviceAssignmentService)
    {
        _databaseRepository = databaseRepository.ThrowIfNull();
        _deviceAssignmentService = deviceAssignmentService.ThrowIfNull();
    }
    
    public async Task<GetDevicesAdminResponse> Handle(GetDevicesAdminRequest request, CancellationToken cancellationToken)
    {
        var deviceIds = await _deviceAssignmentService.GetDevicesAssignedToUserAsync(
            request.UserId.ToGuid(), request.Offset, request.Limit, cancellationToken);
        
        var query = _databaseRepository.Devices.AsQueryable()
            .Where(x => deviceIds.Contains(x.DeviceId));

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