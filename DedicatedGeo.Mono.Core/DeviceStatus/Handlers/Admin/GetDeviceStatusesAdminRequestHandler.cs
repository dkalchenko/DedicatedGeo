using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Entities.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device;

public class GetDeviceStatusesAdminRequestHandler: IRequestHandler<GetDeviceStatusesAdminRequest, GetDeviceStatusesAdminResponse>
{
    
    private readonly IDatabaseRepository _repository;
    private readonly ISender _sender;

    public GetDeviceStatusesAdminRequestHandler(IDatabaseRepository repository, ISender sender)
    {
        _repository = repository.ThrowIfNull();
        _sender = sender.ThrowIfNull();
    }
    
    public async Task<GetDeviceStatusesAdminResponse> Handle(GetDeviceStatusesAdminRequest request, CancellationToken cancellationToken)
    {
        
        var device = await _sender.Send(new GetDeviceInternalRequest
        {
            DeviceId = request.DeviceId
        }, cancellationToken);

        if (device is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("device").GetException();
        }
        
        var deviceStatus = await _repository.DeviceStatuses
            .AsQueryable()
            .Where(x => x.DeviceId == request.DeviceId.ToGuid())
            .OrderByDescending(x => x.UpdatedAt)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (deviceStatus is null)
        {
            return new GetDeviceStatusesAdminResponse();
        }
        
        return new GetDeviceStatusesAdminResponse
        {
            IsGPSOnline = deviceStatus.IsGPSOnline,
            BatteryLevel = deviceStatus.BatteryLevel,
            IsButtonPressed = deviceStatus.IsButtonPressed,
            IsInAlarm = deviceStatus.IsInAlarm,
            IsInCharge = deviceStatus.IsInCharge,
            IsDeviceOnline = deviceStatus.IsDeviceOnline,
            UpdatedAt = deviceStatus.UpdatedAt,
        };
    }
}