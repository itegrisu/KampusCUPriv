using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Create;

public class CreateVehicleAccidentCommandValidator : AbstractValidator<CreateVehicleAccidentCommand>
{
    public CreateVehicleAccidentCommandValidator()
    {
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();

        RuleFor(c => c.AccidentDate).NotNull().NotEmpty();
        RuleFor(c => c.Driver).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.AccidentFile).MaximumLength(150);
        RuleFor(c => c.AccidentImageFile).MaximumLength(150);
    }
}