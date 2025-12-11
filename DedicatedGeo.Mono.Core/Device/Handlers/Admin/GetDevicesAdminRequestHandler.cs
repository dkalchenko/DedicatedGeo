using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device.Handlers.Admin;

public class GetDevicesAdminRequestHandler: IRequestHandler<GetDevicesAdminRequest, GetDevicesAdminResponse>
{
    private readonly IDatabaseRepository _databaseRepository;
    public GetDevicesAdminRequestHandler(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository.ThrowIfNull();
    }
    
    public async Task<GetDevicesAdminResponse> Handle(GetDevicesAdminRequest request, CancellationToken cancellationToken)
    {
        var devices = await _databaseRepository.Devices.AsQueryable()
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(cancellationToken: cancellationToken);

        return new GetDevicesAdminResponse
        {
            Devices = devices.Select(x => new GetDeviceAdminResponse
            {
                DeviceId = x.DeviceId,
                DeviceName = x.Name,
            })
        };
    }
}