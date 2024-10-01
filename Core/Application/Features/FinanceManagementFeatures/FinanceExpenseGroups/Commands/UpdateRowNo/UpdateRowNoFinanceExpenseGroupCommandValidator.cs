using Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.UpdateRowNo;
using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseGroups.Commands.Create;

public class UpdateRowNoFinanceExpenseGroupCommandValidator : AbstractValidator<UpdateRowNoFinanceExpenseGroupCommand>
{
    public UpdateRowNoFinanceExpenseGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}