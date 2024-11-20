using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleRequests.Commands.Delete;

public class DeleteVehicleRequestCommandValidator : AbstractValidator<DeleteVehicleRequestCommand>
{
    public DeleteVehicleRequestCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}