using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleInspections.Commands.Create;

public class CreateVehicleInspectionCommandValidator : AbstractValidator<CreateVehicleInspectionCommand>
{
    public CreateVehicleInspectionCommandValidator()
    {
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();

RuleFor(c => c.StartDate).NotNull().NotEmpty();
RuleFor(c => c.EndDate).NotNull().NotEmpty();
RuleFor(c => c.DocumentFile).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(250);


    }
}