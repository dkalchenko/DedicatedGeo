using DedicatedGeo.Mono.Dtos.Device;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.DeviceStatus.Validators;

public class PutDeviceStatusS2DRequestValidator: AbstractValidator<PutDeviceStatusS2DRequest>
{
    public PutDeviceStatusS2DRequestValidator()
    {
        RuleFor(request => request.DeviceId).NotNull().NotEmpty();
    }
}