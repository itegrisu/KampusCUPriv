using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Update;

public class UpdateFinanceExpenseGroupCommandValidator : AbstractValidator<UpdateFinanceExpenseGroupCommand>
{
    public UpdateFinanceExpenseGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Description).MaximumLength(100);
        RuleFor(c => c.ExpenseGroupStatus).NotNull().NotEmpty();


    }
}