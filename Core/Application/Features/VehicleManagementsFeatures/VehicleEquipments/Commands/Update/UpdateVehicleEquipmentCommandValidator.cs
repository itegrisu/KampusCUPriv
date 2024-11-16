using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Update;

public class UpdateVehicleEquipmentCommandValidator : AbstractValidator<UpdateVehicleEquipmentCommand>
{
    public UpdateVehicleEquipmentCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();

RuleFor(c => c.EquipmentName).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.DocumentFile).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(250);


    }
}