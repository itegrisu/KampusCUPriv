using FluentValidation;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Commands.Update;

public class UpdateStockCardImageCommandValidator : AbstractValidator<UpdateStockCardImageCommand>
{
    public UpdateStockCardImageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidStockCardFK).NotNull().NotEmpty();
        RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Image).MaximumLength(150);


    }
}