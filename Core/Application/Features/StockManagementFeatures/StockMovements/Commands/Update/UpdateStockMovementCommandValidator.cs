using FluentValidation;

namespace Application.Features.StockManagementFeatures.StockMovements.Commands.Update;

public class UpdateStockMovementCommandValidator : AbstractValidator<UpdateStockMovementCommand>
{
    public UpdateStockMovementCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidStockCardFK).NotNull().NotEmpty();
//RuleFor(c => c.GidPreviousWarehouseFK);//
//RuleFor(c => c.GidNextWarehouseFK);//

RuleFor(c => c.OperationType).NotNull().NotEmpty();
RuleFor(c => c.MovementType).NotNull().NotEmpty();
RuleFor(c => c.TransactionDate).NotNull().NotEmpty();
RuleFor(c => c.Amount).NotNull().NotEmpty();
RuleFor(c => c.Document).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(500);


    }
}