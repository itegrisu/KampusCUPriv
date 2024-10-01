using FluentValidation;

namespace Application.Features.WarehouseManagementFeatures.StockCards.Commands.Delete;

public class DeleteStockCardCommandValidator : AbstractValidator<DeleteStockCardCommand>
{
    public DeleteStockCardCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}