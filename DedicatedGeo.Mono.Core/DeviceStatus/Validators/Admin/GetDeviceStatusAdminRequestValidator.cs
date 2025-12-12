using DedicatedGeo.Mono.Dtos.Device;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.DeviceStatus.Validators;

public class GetDeviceStatusAdminRequestValidator: AbstractValidator<GetDeviceStatusesAdminRequest>
{
    public GetDeviceStatusAdminRequestValidator()
    {
        RuleFor(request => request.DeviceId).NotNull().NotEmpty();
    }
}