using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Commands.Update;

public class UpdateCurrencyCommandValidator : AbstractValidator<UpdateCurrencyCommand>
{
    public UpdateCurrencyCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Code).MaximumLength(20);
        RuleFor(c => c.Symbol).MaximumLength(5);

    }
}