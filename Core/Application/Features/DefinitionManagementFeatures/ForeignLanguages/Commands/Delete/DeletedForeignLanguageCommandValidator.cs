using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Delete;

public class DeleteForeignLanguageCommandValidator : AbstractValidator<DeleteForeignLanguageCommand>
{
    public DeleteForeignLanguageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}