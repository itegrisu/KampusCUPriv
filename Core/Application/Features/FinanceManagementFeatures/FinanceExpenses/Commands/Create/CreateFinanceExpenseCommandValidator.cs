using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.Create;

public class CreateFinanceExpenseCommandValidator : AbstractValidator<CreateFinanceExpenseCommand>
{
    public CreateFinanceExpenseCommandValidator()
    {
        RuleFor(c => c.GidExpenseGroupFK).NotNull().NotEmpty();
//RuleFor(c => c.GidOrganizationFK);//
RuleFor(c => c.GidMoneySenderPersonnelFK).NotNull().NotEmpty();
RuleFor(c => c.GidMoneyReceivePersonnelFK).NotNull().NotEmpty();
RuleFor(c => c.GidCurrencyFK).NotNull().NotEmpty();
//RuleFor(c => c.GidApprovalReceiverFK);//

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.AmountSpent).NotNull().NotEmpty();
RuleFor(c => c.TransactionDate).NotNull().NotEmpty();
RuleFor(c => c.ExpenseStatus).NotNull().NotEmpty();
RuleFor(c => c.Document).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(250);
RuleFor(c => c.ReceiverAcceptStatus).NotNull().NotEmpty();
RuleFor(c => c.ReceiverIpAddress).MaximumLength(20);


    }
}