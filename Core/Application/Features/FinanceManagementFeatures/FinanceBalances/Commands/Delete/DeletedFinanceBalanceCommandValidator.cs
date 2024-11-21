using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Delete;

public class DeleteFinanceBalanceCommandValidator : AbstractValidator<DeleteFinanceBalanceCommand>
{
    public DeleteFinanceBalanceCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}