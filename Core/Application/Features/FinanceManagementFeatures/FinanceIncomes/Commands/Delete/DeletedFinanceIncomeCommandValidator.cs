using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Delete;

public class DeleteFinanceIncomeCommandValidator : AbstractValidator<DeleteFinanceIncomeCommand>
{
    public DeleteFinanceIncomeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}