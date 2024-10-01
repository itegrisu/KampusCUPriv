using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Delete;

public class DeleteFinanceExpenseCommandValidator : AbstractValidator<DeleteFinanceExpenseCommand>
{
    public DeleteFinanceExpenseCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}