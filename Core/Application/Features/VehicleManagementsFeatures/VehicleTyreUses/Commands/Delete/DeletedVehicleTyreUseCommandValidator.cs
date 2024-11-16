using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleTyreUses.Commands.Delete;

public class DeleteVehicleTyreUseCommandValidator : AbstractValidator<DeleteVehicleTyreUseCommand>
{
    public DeleteVehicleTyreUseCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}