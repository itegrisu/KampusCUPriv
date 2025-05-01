using FluentValidation;

namespace Application.Features.DefinitionFeatures.Classes.Commands.Delete;

public class DeleteClassCommandValidator : AbstractValidator<DeleteClassCommand>
{
    public DeleteClassCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}