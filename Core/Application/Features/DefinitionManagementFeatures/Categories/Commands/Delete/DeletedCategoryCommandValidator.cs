using FluentValidation;

namespace Application.Features.DefinitionFeatures.Categories.Commands.Delete;

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}