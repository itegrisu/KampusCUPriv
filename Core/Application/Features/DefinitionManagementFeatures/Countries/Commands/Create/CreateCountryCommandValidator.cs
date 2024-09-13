using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Countries.Commands.Create;

public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {

        RuleFor(c => c.UlkeAdi).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.UlkeKodu).NotNull().NotEmpty().MaximumLength(5);
        RuleFor(c => c.TelefonKodu).MaximumLength(5);
    }
}