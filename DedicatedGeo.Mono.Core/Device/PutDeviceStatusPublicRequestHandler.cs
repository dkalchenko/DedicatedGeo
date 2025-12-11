using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Models.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device;

public class PutDeviceStatusPublicRequestHandler: IRequestHandler<PutDeviceStatusPublicRequest>
{
    private readonly IDatabaseRepository _repository;

    public PutDeviceStatusPublicRequestHandler(IDatabaseRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }
    
    public async Task Handle(PutDeviceStatusPublicRequest request, CancellationToken cancellationToken)
    {
        var deviceStatus = await _repository.DeviceStatuses.FirstOrDefaultAsync(x => x.DeviceStatusId == Guid.Parse(OwnConstants.DeviceId), cancellationToken: cancellationToken);

        if (deviceStatus is null)
        {
            deviceStatus = new DeviceStatus
            {
                DeviceStatusId = Guid.Parse(OwnConstants.DeviceId)
            };
            _repository.DeviceStatuses.Attach(deviceStatus);
        }
        
        deviceStatus.BatteryLevel = request.BatteryLevel;
        deviceStatus.IsButtonPressed = request.IsButtonPressed;
        deviceStatus.IsInAlarm = request.IsInAlarm;
        deviceStatus.IsInCharge = request.IsInCharge;
        deviceStatus.IsGPSOnline = request.IsGPSOnline;
        deviceStatus.UpdatedAt = DateTime.UtcNow;

        await _repository.SaveChangesAsync(cancellationToken);
    }
}