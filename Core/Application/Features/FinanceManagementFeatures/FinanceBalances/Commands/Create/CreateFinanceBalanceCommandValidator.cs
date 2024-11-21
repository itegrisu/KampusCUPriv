using FluentValidation;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.Create;

public class CreateFinanceBalanceCommandValidator : AbstractValidator<CreateFinanceBalanceCommand>
{
    public CreateFinanceBalanceCommandValidator()
    {
        RuleFor(c => c.GidSupplierCustomerFK).NotNull().NotEmpty();
//RuleFor(c => c.GidVehicleTransactionFK);//
//RuleFor(c => c.GidTransportationFK);//
//RuleFor(c => c.GidTransportationExternalServiceFK);//
RuleFor(c => c.GidFeeCurrencyFK).NotNull().NotEmpty();

RuleFor(c => c.BalanceType).NotNull().NotEmpty();
RuleFor(c => c.BalanceResourceType).NotNull().NotEmpty();
RuleFor(c => c.ExpirationDate).NotNull().NotEmpty();
RuleFor(c => c.Fee).NotNull().NotEmpty();
RuleFor(c => c.PaymentStatus).NotNull().NotEmpty();
RuleFor(c => c.PaymentFile).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(250);


    }
}