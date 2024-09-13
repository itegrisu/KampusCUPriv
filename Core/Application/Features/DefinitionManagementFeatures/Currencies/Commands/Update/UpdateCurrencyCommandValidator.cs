using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Commands.Update;

public class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand>
{
    public UpdateCurrencyCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.DovizAdi).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.DovizKodu).MaximumLength(20);
        RuleFor(c => c.DovizSimgesi).MaximumLength(5);

    }
}