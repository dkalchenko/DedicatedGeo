using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.Device.Admin;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.Device.Validators.Admin;

public class GetDeviceAdminRequestValidator: AbstractValidator<GetDeviceAdminRequest>
{
    public GetDeviceAdminRequestValidator()
    {
        RuleFor(x => x.UserId).MustBeGuid();
        RuleFor(x => x.DeviceId).MustBeGuid();
    }
}