using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Delete;

public class DeleteStockCategoryCommandValidator : AbstractValidator<DeleteStockCategoryCommand>
{
    public DeleteStockCategoryCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}