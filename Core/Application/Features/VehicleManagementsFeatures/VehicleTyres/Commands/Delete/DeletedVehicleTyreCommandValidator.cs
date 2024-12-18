using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.Tyres.Commands.Delete;

public class DeleteTyreCommandValidator : AbstractValidator<DeleteVehicleTyreCommand>
{
    public DeleteTyreCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}