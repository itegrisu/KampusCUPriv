using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Update;

public class UpdateVehicleAccidentCommandValidator : AbstractValidator<UpdateVehicleAccidentCommand>
{
    public UpdateVehicleAccidentCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();
        RuleFor(c => c.AccidentDate).NotNull().NotEmpty();
        RuleFor(c => c.Driver).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.AccidentFile).MaximumLength(150);
        RuleFor(c => c.AccidentImageFile).MaximumLength(150);
    }
}