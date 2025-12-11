using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Entities.Device;
using DedicatedGeo.Mono.Models.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device;

public class PutDeviceStatusS2DRequestHandler: IRequestHandler<PutDeviceStatusS2DRequest>
{
    private readonly IDatabaseRepository _repository;
    private readonly ISender _sender;

    public PutDeviceStatusS2DRequestHandler(IDatabaseRepository repository, ISender sender)
    {
        _repository = repository.ThrowIfNull();
        _sender = sender.ThrowIfNull();
    }
    
    public async Task Handle(PutDeviceStatusS2DRequest request, CancellationToken cancellationToken)
    {
        var device = await _sender.Send(new GetDeviceByIMEIInternalRequest
        {
            DeviceId = request.DeviceId
        }, cancellationToken);
        
        var deviceStatus = await _repository.DeviceStatuses.FirstOrDefaultAsync(x => x.DeviceId == device.DeviceId, cancellationToken: cancellationToken);

        if (deviceStatus is null)
        {
            deviceStatus = new Models.Device.DeviceStatus
            {
                DeviceId = device.DeviceId,
                DeviceStatusId = Guid.NewGuid(),
                BatteryLevel = request.BatteryLevel,
                IsButtonPressed = request.IsButtonPressed,
                IsInAlarm = request.IsInAlarm,
                IsInCharge = request.IsInCharge,
                IsGPSOnline = request.IsGPSOnline,
                UpdatedAt = DateTime.UtcNow
            };
            _repository.DeviceStatuses.Add(deviceStatus);
        }
        else
        {
            deviceStatus.BatteryLevel = request.BatteryLevel;
            deviceStatus.IsButtonPressed = request.IsButtonPressed;
            deviceStatus.IsInAlarm = request.IsInAlarm;
            deviceStatus.IsInCharge = request.IsInCharge;
            deviceStatus.IsGPSOnline = request.IsGPSOnline;
            deviceStatus.UpdatedAt = DateTime.UtcNow;    
        }
        

        await _repository.SaveChangesAsync(cancellationToken);
    }
}