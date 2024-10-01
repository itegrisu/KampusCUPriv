using FluentValidation;

namespace Application.Features.WareHouseManagementFeatures.StockMovements.Commands.UploadFile;

public class UploadStockMovementCommandValidator : AbstractValidator<UploadStockMovementCommand>
{
    public UploadStockMovementCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();

    }
}