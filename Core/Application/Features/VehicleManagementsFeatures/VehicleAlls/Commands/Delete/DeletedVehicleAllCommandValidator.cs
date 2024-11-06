using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleAlls.Commands.Delete;

public class DeleteVehicleAllCommandValidator : AbstractValidator<DeleteVehicleAllCommand>
{
    public DeleteVehicleAllCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}