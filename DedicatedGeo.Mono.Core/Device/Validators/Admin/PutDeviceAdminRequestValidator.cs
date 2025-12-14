using DedicatedGeo.Mono.Dtos.Device;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.Device.Validators.Admin;

public class PutDeviceAdminRequestValidator: AbstractValidator<PutDeviceAdminRequest>
{
    public PutDeviceAdminRequestValidator()
    {
        RuleFor(x => x.DeviceName).NotEmpty().MaximumLength(120);
        RuleFor(x => x.DeviceId).NotEmpty();
    }
}