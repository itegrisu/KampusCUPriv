using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Create;

public class CreateVehicleAllCommandValidator : AbstractValidator<CreateVehicleAllCommand>
{
    public CreateVehicleAllCommandValidator()
    {
        RuleFor(c => c.GidVehicleBrand).NotNull().NotEmpty();
        RuleFor(c => c.PlateNumber).NotNull().NotEmpty().MaximumLength(15);
        RuleFor(c => c.VehicleType).NotNull().NotEmpty();
        RuleFor(c => c.Model).MaximumLength(60);
        RuleFor(c => c.Color).MaximumLength(30);
        RuleFor(c => c.EngineNo).MaximumLength(50);
        RuleFor(c => c.ChassisNumber).MaximumLength(50);
        RuleFor(c => c.PassengerCount).NotNull().NotEmpty();
        RuleFor(c => c.FuelType).NotNull().NotEmpty();
        RuleFor(c => c.Description).MaximumLength(250);


    }
}