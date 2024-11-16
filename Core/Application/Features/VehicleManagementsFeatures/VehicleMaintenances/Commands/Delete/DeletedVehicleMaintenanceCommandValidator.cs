using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Delete;

public class DeleteVehicleMaintenanceCommandValidator : AbstractValidator<DeleteVehicleMaintenanceCommand>
{
    public DeleteVehicleMaintenanceCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}