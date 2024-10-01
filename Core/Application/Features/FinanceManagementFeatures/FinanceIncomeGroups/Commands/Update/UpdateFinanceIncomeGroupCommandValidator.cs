using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Update;

public class UpdateFinanceIncomeGroupCommandValidator : AbstractValidator<UpdateFinanceIncomeGroupCommand>
{
    public UpdateFinanceIncomeGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();

        RuleFor(c => c.IncomeGroupName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Description).MaximumLength(100);
        RuleFor(c => c.IncomeGroupStatus).NotNull().NotEmpty();



    }
}