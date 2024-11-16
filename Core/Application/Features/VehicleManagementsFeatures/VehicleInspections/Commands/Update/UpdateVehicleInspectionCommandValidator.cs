using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Update;

public class UpdateVehicleInspectionCommandValidator : AbstractValidator<UpdateVehicleInspectionCommand>
{
    public UpdateVehicleInspectionCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();

RuleFor(c => c.StartDate).NotNull().NotEmpty();
RuleFor(c => c.EndDate).NotNull().NotEmpty();
RuleFor(c => c.DocumentFile).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(250);


    }
}