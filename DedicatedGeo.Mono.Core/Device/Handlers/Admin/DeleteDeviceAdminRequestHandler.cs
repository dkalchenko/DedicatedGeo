using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device.Handlers.Admin;

public class DeleteDeviceAdminRequestHandler: IRequestHandler<DeleteDeviceAdminRequest>
{
    
    private readonly IDatabaseRepository _repository;

    public DeleteDeviceAdminRequestHandler(IDatabaseRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }
    
    public async Task Handle(DeleteDeviceAdminRequest request, CancellationToken cancellationToken)
    {
        var deviceId = request.DeviceId.ToGuid();
        var existedDevice = await _repository.Devices.FirstOrDefaultAsync(x => x.DeviceId == deviceId, cancellationToken: cancellationToken);

        if (existedDevice is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("device").GetException();
        }
        
        _repository.Devices.Remove(existedDevice);
        await _repository.SaveChangesAsync(cancellationToken);
    }
}