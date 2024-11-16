using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleEquipments.Commands.Delete;

public class DeleteVehicleEquipmentCommandValidator : AbstractValidator<DeleteVehicleEquipmentCommand>
{
    public DeleteVehicleEquipmentCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}