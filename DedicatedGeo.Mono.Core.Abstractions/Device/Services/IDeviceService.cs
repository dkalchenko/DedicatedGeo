using DedicatedGeo.Mono.Entities.Device;

namespace DedicatedGeo.Mono.Core.Abstractions.Device.Services;

public interface IDeviceService
{
    public Task<DeviceEntity?> GetDeviceByIdAsync(Guid deviceId, CancellationToken cancellationToken);
    public Task<DeviceEntity?> GetDeviceByIMEIAsync(string IMEI, CancellationToken cancellationToken);
}