using FluentValidation;

namespace Application.Features.StockManagementFeatures.StockCardImages.Commands.UploadFile;

public class UploadStockCardImageCommandValidator : AbstractValidator<UploadStockCardImageCommand>
{
    public UploadStockCardImageCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();

    }
}