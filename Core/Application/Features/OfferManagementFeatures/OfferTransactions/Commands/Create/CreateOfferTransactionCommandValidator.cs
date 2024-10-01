using FluentValidation;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Create;

public class CreateOfferTransactionCommandValidator : AbstractValidator<CreateOfferTransactionCommand>
{
    public CreateOfferTransactionCommandValidator()
    {
        RuleFor(c => c.GidOfferFK).NotNull().NotEmpty();
RuleFor(c => c.GidCurrencyFK).NotNull().NotEmpty();

RuleFor(c => c.OfferId).NotNull().NotEmpty().MaximumLength(20);
RuleFor(c => c.Total).NotNull().NotEmpty();
RuleFor(c => c.Document).MaximumLength(150);
RuleFor(c => c.Description).MaximumLength(250);


    }
}