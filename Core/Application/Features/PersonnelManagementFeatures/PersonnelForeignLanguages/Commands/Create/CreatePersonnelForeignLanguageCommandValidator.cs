using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Create;

public class CreatePersonnelForeignLanguageCommandValidator : AbstractValidator<CreatePersonnelForeignLanguageCommand>
{
    public CreatePersonnelForeignLanguageCommandValidator()
    {
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();
        RuleFor(c => c.GidLanguageFK).NotNull().NotEmpty();

        RuleFor(c => c.SpeakingLevel).NotNull().NotEmpty();
        RuleFor(c => c.ReadLevel).NotNull().NotEmpty();


    }
}