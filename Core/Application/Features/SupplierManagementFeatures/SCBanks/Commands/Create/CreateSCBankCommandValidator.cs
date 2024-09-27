using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Create;

public class CreateSCBankCommandValidator : AbstractValidator<CreateSCBankCommand>
{
    public CreateSCBankCommandValidator()
    {
        RuleFor(c => c.GidSCCompanyFK).NotNull().NotEmpty();
        RuleFor(c => c.GidCurrencyFK).NotNull().NotEmpty();

        RuleFor(c => c.Bank).NotNull().NotEmpty().MaximumLength(60);
        RuleFor(c => c.BranchName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.BranchCode).MaximumLength(20);
        RuleFor(c => c.AccountNumber).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.IbanNo).MaximumLength(50);
        RuleFor(c => c.SwiftNo).MaximumLength(50);


    }
}