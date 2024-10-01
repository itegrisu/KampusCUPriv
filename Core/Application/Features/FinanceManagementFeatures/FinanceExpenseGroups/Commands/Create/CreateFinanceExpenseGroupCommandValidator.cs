using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Create;

public class CreateFinanceExpenseGroupCommandValidator : AbstractValidator<CreateFinanceExpenseGroupCommand>
{
    public CreateFinanceExpenseGroupCommandValidator()
    {

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Description).MaximumLength(100);
        RuleFor(c => c.ExpenseGroupStatus).NotNull().NotEmpty();

    }
}