using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Common.Extensions;
using DedicatedGeo.Mono.Dal.Abstractions;
using DedicatedGeo.Mono.Dtos.Device;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DedicatedGeo.Mono.Core.Device.Handlers.Admin;

public class PostDeviceAdminRequestHandler: IRequestHandler<PostDeviceAdminRequest, GetDeviceAdminResponse>
{
    private readonly IDatabaseRepository _repository;

    public PostDeviceAdminRequestHandler(IDatabaseRepository repository)
    {
        _repository = repository.ThrowIfNull();
    }
    
    public async Task<GetDeviceAdminResponse> Handle(PostDeviceAdminRequest request, CancellationToken cancellationToken)
    {
        var existedDevice = await _repository.Devices.FirstOrDefaultAsync(x => x.IMEI == request.IMEI, cancellationToken: cancellationToken);

        if (existedDevice is not null)
        {
            throw OwnConstants.ErrorTemplates.ResourceAlreadyExists.GetException();
        }

        var deviceId = Guid.NewGuid();
        
        var newDevice = new Models.Device.Device
        {
            DeviceId = deviceId,
            IMEI = request.IMEI,
            Name = request.DeviceName,
            DeviceAssignments =
            [
                new Models.Device.DeviceAssignment
                {
                    DeviceId = deviceId,
                    UserId = request.UserId.ToGuid(),
                    CreatedAt = DateTime.UtcNow,
                    DeviceAssignmentId = Guid.NewGuid()
                }
            ]
        };

        _repository.Devices.Add(newDevice);
        
        await _repository.SaveChangesAsync(cancellationToken);
        
        return new GetDeviceAdminResponse
        {
            DeviceId = newDevice.DeviceId,
            IMEI = newDevice.IMEI,
            DeviceName = newDevice.Name
        };
    }
}