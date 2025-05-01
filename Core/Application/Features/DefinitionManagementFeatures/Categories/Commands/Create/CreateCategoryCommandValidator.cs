using FluentValidation;

namespace Application.Features.DefinitionFeatures.Categories.Commands.Create;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);


    }
}