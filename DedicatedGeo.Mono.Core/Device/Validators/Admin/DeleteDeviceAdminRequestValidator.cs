using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.Device;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.Device.Validators.Admin;

public class DeleteDeviceAdminRequestValidator: AbstractValidator<DeleteDeviceAdminRequest>
{
    public DeleteDeviceAdminRequestValidator()
    {
        RuleFor(x => x.DeviceId).MustBeGuid();
    }
}