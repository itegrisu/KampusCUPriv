using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.ForeignLanguages.Commands.Update;

public class UpdateForeignLanguageCommandValidator : AbstractValidator<UpdateForeignLanguageCommand>
{
    public UpdateForeignLanguageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.DilAdi).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.DilKodu).MaximumLength(5);


    }
}