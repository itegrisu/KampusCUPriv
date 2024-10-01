using Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.UpdateRowNo;
using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Create;

public class UpdateRowNoFinanceIncomeGroupCommandValidator : AbstractValidator<UpdateRowNoFinanceIncomeGroupCommand>
{
    public UpdateRowNoFinanceIncomeGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}