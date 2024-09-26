using FluentValidation;

namespace Application.Features.StockManagementFeatures.StockCards.Commands.Delete;

public class DeleteStockCardCommandValidator : AbstractValidator<DeleteStockCardCommand>
{
    public DeleteStockCardCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}