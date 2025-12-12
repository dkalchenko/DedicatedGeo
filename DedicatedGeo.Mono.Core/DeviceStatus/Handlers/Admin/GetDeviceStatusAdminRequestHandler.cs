using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Entities.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device;

public class GetDeviceStatusAdminRequestHandler: IRequestHandler<GetDeviceStatusAdminRequest, GetDeviceStatusAdminResponse>
{
    
    private readonly IDatabaseRepository _repository;
    private readonly ISender _sender;

    public GetDeviceStatusAdminRequestHandler(IDatabaseRepository repository, ISender sender)
    {
        _repository = repository.ThrowIfNull();
        _sender = sender.ThrowIfNull();
    }
    
    public async Task<GetDeviceStatusAdminResponse> Handle(GetDeviceStatusAdminRequest request, CancellationToken cancellationToken)
    {
        
        var device = await _sender.Send(new GetDeviceInternalRequest
        {
            DeviceId = request.DeviceId
        }, cancellationToken);

        if (device is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("device").GetException();
        }
        
        var deviceStatus = await _repository.DeviceStatuses.FirstOrDefaultAsync(x => x.DeviceId == request.DeviceId.ToGuid(), cancellationToken: cancellationToken);

        if (deviceStatus is null)
        {
            return new GetDeviceStatusAdminResponse();
        }
        
        return new GetDeviceStatusAdminResponse
        {
            IsGPSOnline = deviceStatus.IsGPSOnline,
            BatteryLevel = deviceStatus.BatteryLevel,
            IsButtonPressed = deviceStatus.IsButtonPressed,
            IsInAlarm = deviceStatus.IsInAlarm,
            IsInCharge = deviceStatus.IsInCharge,
            IsDeviceOnline = deviceStatus.UpdatedAt.IsWithinLastMinutes(2),
            UpdatedAt = deviceStatus.UpdatedAt,
        };
    }
}