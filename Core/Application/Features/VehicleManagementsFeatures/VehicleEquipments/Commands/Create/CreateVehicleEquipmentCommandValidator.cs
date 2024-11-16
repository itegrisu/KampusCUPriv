using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Create;

public class CreateVehicleEquipmentCommandValidator : AbstractValidator<CreateVehicleEquipmentCommand>
{
    public CreateVehicleEquipmentCommandValidator()
    {
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();

RuleFor(c => c.EquipmentName).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.DocumentFile).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(250);


    }
}