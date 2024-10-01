using FluentValidation;

namespace Application.Features.WareHouseManagementFeatures.StockCardImages.Commands.UpdateRowNo;

public class UpdateRowNoStockCardImageCommandValidator : AbstractValidator<UpdateRowNoStockCardImageCommand>
{
    public UpdateRowNoStockCardImageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}