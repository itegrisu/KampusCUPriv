using Application.Features.StockManagementFeatures.StockCardImages.Commands.UpdateRowNo;
using FluentValidation;

namespace Application.Features.StockManagementFeatures.StockCardImages.Commands.Create;

public class UpdateRowNoStockCardImageCommandValidator : AbstractValidator<UpdateRowNoStockCardImageCommand>
{
    public UpdateRowNoStockCardImageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}