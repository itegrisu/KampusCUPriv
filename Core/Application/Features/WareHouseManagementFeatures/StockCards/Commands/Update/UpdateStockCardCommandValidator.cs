using FluentValidation;

namespace Application.Features.WarehouseManagementFeatures.StockCards.Commands.Update;

public class UpdateStockCardCommandValidator : AbstractValidator<UpdateStockCardCommand>
{
    public UpdateStockCardCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidStockCategoryFK).NotNull().NotEmpty();
RuleFor(c => c.GidMeasureFK).NotNull().NotEmpty();

RuleFor(c => c.StockType).NotNull().NotEmpty();
RuleFor(c => c.StockName).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.StockCode).MaximumLength(20);
RuleFor(c => c.Brand).MaximumLength(50);
RuleFor(c => c.Description).MaximumLength(250);


    }
}