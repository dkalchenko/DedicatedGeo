using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device;

public class GetDeviceStatusPublicRequestHandler: IRequestHandler<GetDeviceStatusPublicRequest, GetDeviceStatusPublicResponse>
{
    
    private readonly IDatabaseRepository _repository;

    public GetDeviceStatusPublicRequestHandler(IDatabaseRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }
    
    public async Task<GetDeviceStatusPublicResponse> Handle(GetDeviceStatusPublicRequest request, CancellationToken cancellationToken)
    {
        var deviceStatus = await _repository.DeviceStatuses.FirstOrDefaultAsync(x => x.DeviceStatusId == Guid.Parse(OwnConstants.DeviceId), cancellationToken: cancellationToken);

        if (deviceStatus is null)
        {
            return new GetDeviceStatusPublicResponse();
        }

        return new GetDeviceStatusPublicResponse
        {
            IsGPSOnline = deviceStatus.IsGPSOnline,
            BatteryLevel = deviceStatus.BatteryLevel,
            IsButtonPressed = deviceStatus.IsButtonPressed,
            IsInAlarm = deviceStatus.IsInAlarm,
            IsInCharge = deviceStatus.IsInCharge,
            UpdatedAt = deviceStatus.UpdatedAt,
        };
    }
}