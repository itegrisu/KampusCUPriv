using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Delete;

public class DeleteFinanceExpenseGroupCommandValidator : AbstractValidator<DeleteFinanceExpenseGroupCommand>
{
    public DeleteFinanceExpenseGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}