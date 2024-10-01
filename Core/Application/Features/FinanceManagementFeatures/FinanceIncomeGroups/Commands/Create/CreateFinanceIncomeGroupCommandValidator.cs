using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomeGroups.Commands.Create;

public class CreateFinanceIncomeGroupCommandValidator : AbstractValidator<CreateFinanceIncomeGroupCommand>
{
    public CreateFinanceIncomeGroupCommandValidator()
    {

        RuleFor(c => c.IncomeGroupName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Description).MaximumLength(100);
        RuleFor(c => c.IncomeGroupStatus).NotNull().NotEmpty();

    }
}