using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.Device.Admin.DeviceAssignment;
using DedicatedGeo.Mono.Dtos.Device.DeviceAssignment;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.DeviceAssignment.Validators;

public class PostDeviceAssignmentAdminRequestValidator: AbstractValidator<PostDeviceAssignmentAdminRequest>
{
    public PostDeviceAssignmentAdminRequestValidator()
    {
        RuleFor(x => x.DeviceId).MustBeGuid();
        RuleFor(x => x.UserId).MustBeGuid();
    }
}