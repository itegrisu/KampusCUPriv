using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenseDetails.Commands.Update;

public class UpdateFinanceExpenseDetailCommandValidator : AbstractValidator<UpdateFinanceExpenseDetailCommand>
{
    public UpdateFinanceExpenseDetailCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidExpenseFK).NotNull().NotEmpty();
        RuleFor(c => c.GidSpendPersonnelFK).NotNull().NotEmpty();
        RuleFor(c => c.GidCurrencyFK).NotNull().NotEmpty();
        //RuleFor(c => c.GidControlPersonnelFK);//

        RuleFor(c => c.SpentTitle).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Fee).NotNull().NotEmpty();
        RuleFor(c => c.TransactionDate).NotNull().NotEmpty();
        RuleFor(c => c.Document).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);
        RuleFor(c => c.ApprovalStatus).NotNull().NotEmpty();
        RuleFor(c => c.ControlDescription).MaximumLength(250);


    }
}