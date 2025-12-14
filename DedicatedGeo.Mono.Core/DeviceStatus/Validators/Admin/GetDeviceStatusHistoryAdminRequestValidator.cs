using DedicatedGeo.Mono.Common;
using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.Device;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.DeviceStatus.Validators;

public class GetDeviceStatusHistoryAdminRequestValidator: AbstractValidator<GetDeviceStatusHistoryAdminRequest>
{
    public GetDeviceStatusHistoryAdminRequestValidator()
    {
        RuleFor(x => x.StatusName).NotEmpty().Must(x => OwnConstants.DeviceStatusNames.AllNames.Contains(x));
        RuleFor(request => request.DeviceId).MustBeGuid();
        RuleFor(request => request.UserId).MustBeGuid();
    }
}