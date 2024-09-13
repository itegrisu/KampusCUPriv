using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Commands.Create;

public class CreateCurrencyCommandValidator : AbstractValidator<CreateCurrencyCommand>
{
    public CreateCurrencyCommandValidator()
    {
        
RuleFor(c => c.DovizAdi).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.DovizKodu).MaximumLength(20);
RuleFor(c => c.DovizSimgesi).MaximumLength(5);


    }
}