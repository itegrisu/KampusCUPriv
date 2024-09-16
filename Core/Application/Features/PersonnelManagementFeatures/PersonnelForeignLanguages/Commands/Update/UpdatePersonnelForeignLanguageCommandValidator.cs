using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Update;

public class UpdatePersonnelForeignLanguageCommandValidator : AbstractValidator<UpdatePersonnelForeignLanguageCommand>
{
    public UpdatePersonnelForeignLanguageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidPersonelFK).NotNull().NotEmpty();
        RuleFor(c => c.GidLanguageFK).NotNull().NotEmpty();

        RuleFor(c => c.KonusmaDuzeyi).NotNull().NotEmpty();
        RuleFor(c => c.OkumaDuzeyi).NotNull().NotEmpty();


    }
}