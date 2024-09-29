using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Update;

public class UpdatePersonnelForeignLanguageCommandValidator : AbstractValidator<UpdatePersonnelForeignLanguageCommand>
{
    public UpdatePersonnelForeignLanguageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();
        RuleFor(c => c.GidLanguageFK).NotNull().NotEmpty();

        RuleFor(c => c.SpeakingLevel).NotNull().NotEmpty();
        RuleFor(c => c.ReadLevel).NotNull().NotEmpty();


    }
}