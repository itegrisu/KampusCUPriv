using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Create;

public class CreateForeignLanguageCommandValidator : AbstractValidator<CreateForeignLanguageCommand>
{
    public CreateForeignLanguageCommandValidator()
    {
        
RuleFor(c => c.DilAdi).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.DilKodu).MaximumLength(5);


    }
}