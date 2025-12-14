using DedicatedGeo.Mono.Core.Extensions;
using DedicatedGeo.Mono.Dtos.Device.Admin.DeviceAssignment;
using FluentValidation;

namespace DedicatedGeo.Mono.Core.DeviceAssignment.Validators;

public class DeleteDeviceAssignmentAdminRequestValidator: AbstractValidator<DeleteDeviceAssignmentAdminRequest>
{
    public DeleteDeviceAssignmentAdminRequestValidator()
    {
        RuleFor(x => x.DeviceAssignmentId).MustBeGuid();
    }
}