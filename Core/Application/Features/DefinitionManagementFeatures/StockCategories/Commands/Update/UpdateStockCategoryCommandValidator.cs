using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Update;

public class UpdateStockCategoryCommandValidator : AbstractValidator<UpdateStockCategoryCommand>
{
    public UpdateStockCategoryCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Code).MaximumLength(20);


    }
}