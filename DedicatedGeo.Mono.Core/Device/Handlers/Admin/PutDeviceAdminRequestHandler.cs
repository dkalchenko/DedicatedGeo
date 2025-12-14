using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device.Handlers.Admin;

public class PutDeviceAdminRequestHandler: IRequestHandler<PutDeviceAdminRequest, GetDeviceAdminResponse>
{
    private readonly IDatabaseRepository _repository;

    public PutDeviceAdminRequestHandler(IDatabaseRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }
    
    public async Task<GetDeviceAdminResponse> Handle(PutDeviceAdminRequest request, CancellationToken cancellationToken)
    {
        var existedDevice = await _repository.Devices.FirstOrDefaultAsync(x => x.DeviceId == request.DeviceId, cancellationToken: cancellationToken);

        if (existedDevice is null)
        {
            throw OwnConstants.ErrorTemplates.ResourceNotFound.FormatMessage("device").GetException();
        }

        existedDevice.Name = request.DeviceName;

        await _repository.SaveChangesAsync(cancellationToken);
        
        return new GetDeviceAdminResponse
        {
            DeviceId = existedDevice.DeviceId,
            IMEI = existedDevice.IMEI,
            DeviceName = existedDevice.Name
        };
    }
}