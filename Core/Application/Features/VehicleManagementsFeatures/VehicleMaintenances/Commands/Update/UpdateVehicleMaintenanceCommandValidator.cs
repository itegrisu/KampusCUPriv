using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleMaintenances.Commands.Update;

public class UpdateVehicleMaintenanceCommandValidator : AbstractValidator<UpdateVehicleMaintenanceCommand>
{
    public UpdateVehicleMaintenanceCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();

RuleFor(c => c.MaintenanceDate).NotNull().NotEmpty();
RuleFor(c => c.ResponsiblePerson).MaximumLength(100);
RuleFor(c => c.MaintenanceFee).NotNull().NotEmpty();
RuleFor(c => c.DocumentFile).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(250);


    }
}