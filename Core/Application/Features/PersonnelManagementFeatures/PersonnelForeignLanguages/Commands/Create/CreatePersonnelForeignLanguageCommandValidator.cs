using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelForeignLanguages.Commands.Create;

public class CreatePersonnelForeignLanguageCommandValidator : AbstractValidator<CreatePersonnelForeignLanguageCommand>
{
    public CreatePersonnelForeignLanguageCommandValidator()
    {
        RuleFor(c => c.GidPersonelFK).NotNull().NotEmpty();
RuleFor(c => c.GidLanguageFK).NotNull().NotEmpty();

RuleFor(c => c.KonusmaDuzeyi).NotNull().NotEmpty();
RuleFor(c => c.OkumaDuzeyi).NotNull().NotEmpty();


    }
}