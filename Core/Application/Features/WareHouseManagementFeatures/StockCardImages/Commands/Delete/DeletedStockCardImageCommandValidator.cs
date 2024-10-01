using FluentValidation;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Commands.Delete;

public class DeleteStockCardImageCommandValidator : AbstractValidator<DeleteStockCardImageCommand>
{
    public DeleteStockCardImageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}