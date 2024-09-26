using FluentValidation;

namespace Application.Features.StockManagementFeatures.StockCards.Commands.Update;

public class UpdateStockCardCommandValidator : AbstractValidator<UpdateStockCardCommand>
{
    public UpdateStockCardCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        //RuleFor(c => c.GidStockCategoryFK).NotNull().NotEmpty();
        //RuleFor(c => c.GidBrandFK).NotNull().NotEmpty();
        //RuleFor(c => c.GidUnitFK).NotNull().NotEmpty();
        //RuleFor(c => c.GidPriceCurrencyFK).NotNull().NotEmpty();

        //RuleFor(c => c.CardType).NotNull().NotEmpty();
        RuleFor(c => c.StockCode).MaximumLength(50);
        RuleFor(c => c.StockName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Price).NotNull().NotEmpty();
        RuleFor(c => c.TaxRate).NotNull().NotEmpty();
        RuleFor(c => c.Description).MaximumLength(1500);


    }
}