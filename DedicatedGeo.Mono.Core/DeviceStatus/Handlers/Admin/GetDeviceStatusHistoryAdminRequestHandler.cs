using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Core.Abstractions.Common;
using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using DedicatedGeo.Mono.Entities;
using DedicatedGeo.Mono.Entities.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device;

public class GetDeviceStatusHistoryAdminRequestHandler: IRequestHandler<GetDeviceStatusHistoryAdminRequest, GetDeviceStatusHistoryAdminResponse>
{
    
    private readonly IDatabaseRepository _repository;
    private readonly ISender _sender;

    public GetDeviceStatusHistoryAdminRequestHandler(IDatabaseRepository repository, ISender sender)
    {
        _repository = repository.ThrowIfNull();
        _sender = sender.ThrowIfNull();
    }
    
    public async Task<GetDeviceStatusHistoryAdminResponse> Handle(GetDeviceStatusHistoryAdminRequest request, CancellationToken cancellationToken)
    {
        var device = await _sender.Send(new GetDeviceInternalRequest
        {
            DeviceId = request.DeviceId
        }, cancellationToken);

        if (device is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("device").GetException();
        }
        
        var result = await _repository.DeviceStatusHistories
            .Where(x => x.DeviceId == request.DeviceId.ToGuid() && x.StatusName == request.StatusName)
            .OrderByDescending(x => x.ChangedAt)
            .ThenByDescending(x => x.DeviceStatusHistoryId)
            .Skip(request.Offset)
            .Take(request.Limit)
            .ToListAsync(cancellationToken);
        
        
        return new GetDeviceStatusHistoryAdminResponse
        {
            StatusHistory = result.Select(x => new DeviceStatusHistoryItem
            {
                ChangedAt = x.ChangedAt,
                OldValue = x.OldValue,
                NewValue = x.NewValue
            })
        };
    }
}