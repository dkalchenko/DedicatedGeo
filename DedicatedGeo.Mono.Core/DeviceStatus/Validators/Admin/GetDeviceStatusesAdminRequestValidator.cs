using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.Device;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.DeviceStatus.Validators;

public class GetDeviceStatusesAdminRequestValidator: AbstractValidator<GetDeviceStatusesAdminRequest>
{
    public GetDeviceStatusesAdminRequestValidator()
    {
        RuleFor(request => request.DeviceId).MustBeGuid();
        RuleFor(request => request.UserId).MustBeGuid();
    }
}