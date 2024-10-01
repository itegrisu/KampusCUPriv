using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Delete;

public class DeleteFinanceIncomeGroupCommandValidator : AbstractValidator<DeleteFinanceIncomeGroupCommand>
{
    public DeleteFinanceIncomeGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}