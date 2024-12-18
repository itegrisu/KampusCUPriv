using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleAccidents.Commands.Delete;

public class DeleteVehicleAccidentCommandValidator : AbstractValidator<DeleteVehicleAccidentCommand>
{
    public DeleteVehicleAccidentCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}