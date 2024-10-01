using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.StockCategories.Commands.Create;

public class CreateStockCategoryCommandValidator : AbstractValidator<CreateStockCategoryCommand>
{
    public CreateStockCategoryCommandValidator()
    {
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Code).MaximumLength(20);


    }
}