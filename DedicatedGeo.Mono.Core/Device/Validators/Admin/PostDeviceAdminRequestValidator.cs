using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.Device;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.Device.Validators.Admin;

public class PostDeviceAdminRequestValidator: AbstractValidator<PostDeviceAdminRequest>
{
    public PostDeviceAdminRequestValidator()
    {
        RuleFor(x => x.DeviceName).NotEmpty().MaximumLength(120);
        RuleFor(x => x.IMEI).NotEmpty().MaximumLength(16);
        RuleFor(x => x.UserId).MustBeGuid();
    }
}