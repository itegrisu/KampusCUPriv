using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.Update;

public class UpdateFinanceIncomeCommandValidator : AbstractValidator<UpdateFinanceIncomeCommand>
{
    public UpdateFinanceIncomeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
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