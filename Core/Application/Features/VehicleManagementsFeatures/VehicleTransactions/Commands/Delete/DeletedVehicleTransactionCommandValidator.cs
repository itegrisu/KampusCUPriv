using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Delete;

public class DeleteVehicleTransactionCommandValidator : AbstractValidator<DeleteVehicleTransactionCommand>
{
    public DeleteVehicleTransactionCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}