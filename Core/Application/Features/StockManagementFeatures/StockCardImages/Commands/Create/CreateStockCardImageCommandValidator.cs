using FluentValidation;

namespace Application.Features.StockManagementFeatures.StockCardImages.Commands.Create;

public class CreateStockCardImageCommandValidator : AbstractValidator<CreateStockCardImageCommand>
{
    public CreateStockCardImageCommandValidator()
    {
        RuleFor(c => c.GidStockCardFK).NotNull().NotEmpty();
        RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);


    }
}