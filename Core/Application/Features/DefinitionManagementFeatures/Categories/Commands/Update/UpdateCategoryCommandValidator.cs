using FluentValidation;

namespace Application.Features.DefinitionFeatures.Categories.Commands.Update;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);


    }
}