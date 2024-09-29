using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Commands.Create;

public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
{
    public CreateCurrencyCommandValidator()
    {

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Code).MaximumLength(20);
        RuleFor(c => c.Symbol).MaximumLength(5);


    }
}