using FluentValidation;

namespace Application.Features.StockManagementFeatures.StockMovements.Commands.Delete;

public class DeleteStockMovementCommandValidator : AbstractValidator<DeleteStockMovementCommand>
{
    public DeleteStockMovementCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}