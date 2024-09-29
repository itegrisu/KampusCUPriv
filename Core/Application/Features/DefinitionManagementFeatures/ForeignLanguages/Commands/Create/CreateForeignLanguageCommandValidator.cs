using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Create;

public class CreateForeignLanguageCommandValidator : AbstractValidator<CreateForeignLanguageCommand>
{
    public CreateForeignLanguageCommandValidator()
    {

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.LanguageCode).MaximumLength(5);


    }
}