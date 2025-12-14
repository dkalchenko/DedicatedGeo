using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Abstractions.Device.Services;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Entities.Device;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device.Services;

public class DeviceService: IDeviceService
{

    private IDatabaseRepository _databaseRepository;

    public DeviceService(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository.ThrowIfNull();
    }
    
    public async Task<DeviceEntity?> GetDeviceByIdAsync(Guid deviceId, CancellationToken cancellationToken)
    {
        var device = await _databaseRepository.Devices
            .FirstOrDefaultAsync(d => d.DeviceId == deviceId, cancellationToken);

        return device is null? null : new DeviceEntity
        {
            DeviceId = device.DeviceId,
        };
    }

    public async Task<DeviceEntity?> GetDeviceByIMEIAsync(string IMEI, CancellationToken cancellationToken)
    {
        var device = await _databaseRepository.Devices
            .FirstOrDefaultAsync(d => d.IMEI == IMEI, cancellationToken);

        return device is null? null : new DeviceEntity
        {
            DeviceId = device.DeviceId,
        };
    }
}