using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Create;

public class CreateVehicleInsuranceCommandValidator : AbstractValidator<CreateVehicleInsuranceCommand>
{
    public CreateVehicleInsuranceCommandValidator()
    {
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();
        RuleFor(c => c.InsuranceType).NotNull().NotEmpty();
        RuleFor(c => c.StartDate).NotNull().NotEmpty();
        RuleFor(c => c.EndDate).NotNull().NotEmpty();
        RuleFor(c => c.InsuranceFee).NotNull().NotEmpty();
        RuleFor(c => c.DocumentFile).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);
    }
}