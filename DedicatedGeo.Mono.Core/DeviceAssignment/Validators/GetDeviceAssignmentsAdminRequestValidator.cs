using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.Device.DeviceAssignment;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.DeviceAssignment.Validators;

public class GetDeviceAssignmentsAdminRequestValidator: AbstractValidator<GetDeviceAssignmentsAdminRequest>
{
    public GetDeviceAssignmentsAdminRequestValidator()
    {
        RuleFor(x => x.DeviceId).MustBeGuid();
    }
}