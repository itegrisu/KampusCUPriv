using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Countries.Commands.Create;

public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
{
    public CreateCountryCommandValidator()
    {
        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.CountryCode).NotNull().NotEmpty().MaximumLength(5);
        RuleFor(c => c.PhoneCode).MaximumLength(5);

    }
}