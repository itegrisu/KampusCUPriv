using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleInsurances.Commands.Delete;

public class DeleteVehicleInsuranceCommandValidator : AbstractValidator<DeleteVehicleInsuranceCommand>
{
    public DeleteVehicleInsuranceCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}