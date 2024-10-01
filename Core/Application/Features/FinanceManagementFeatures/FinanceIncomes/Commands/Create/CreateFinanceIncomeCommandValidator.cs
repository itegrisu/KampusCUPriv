using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Create;

public class CreateFinanceIncomeCommandValidator : AbstractValidator<CreateFinanceIncomeCommand>
{
    public CreateFinanceIncomeCommandValidator()
    {
        RuleFor(c => c.GidIncomeGroupFK).NotNull().NotEmpty();
RuleFor(c => c.GidCurrencyFK).NotNull().NotEmpty();

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Fee).NotNull().NotEmpty();
RuleFor(c => c.MaturityDate).NotNull().NotEmpty();
RuleFor(c => c.IncomeStatus).NotNull().NotEmpty();
RuleFor(c => c.Document).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(250);


    }
}