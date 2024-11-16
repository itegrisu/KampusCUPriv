using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Delete;

public class DeleteVehicleInspectionCommandValidator : AbstractValidator<DeleteVehicleInspectionCommand>
{
    public DeleteVehicleInspectionCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}