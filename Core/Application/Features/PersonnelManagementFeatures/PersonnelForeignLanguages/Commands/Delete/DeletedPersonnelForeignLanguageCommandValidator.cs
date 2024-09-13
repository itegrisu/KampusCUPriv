using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Delete;

public class DeletePersonnelForeignLanguageCommandValidator : AbstractValidator<DeletePersonnelForeignLanguageCommand>
{
    public DeletePersonnelForeignLanguageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}