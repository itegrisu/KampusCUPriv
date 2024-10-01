using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.Delete;

public class DeleteFinanceExpenseDetailCommandValidator : AbstractValidator<DeleteFinanceExpenseDetailCommand>
{
    public DeleteFinanceExpenseDetailCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}