using FluentValidation;

namespace Application.Features.DefinitionManagementFeatures.Currencies.Commands.Delete;

public class DeleteCurrencyCommandValidator : AbstractValidator<DeleteCurrencyCommand>
{
    public DeleteCurrencyCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}