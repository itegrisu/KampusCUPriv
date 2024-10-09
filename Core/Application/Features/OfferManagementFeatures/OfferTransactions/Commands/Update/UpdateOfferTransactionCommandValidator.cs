using FluentValidation;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Update;

public class UpdateOfferTransactionCommandValidator : AbstractValidator<UpdateOfferTransactionCommand>
{
    public UpdateOfferTransactionCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidOfferFK).NotNull().NotEmpty();
        RuleFor(c => c.GidCurrencyFK).NotNull().NotEmpty();

        RuleFor(c => c.Total).NotNull().NotEmpty();
        RuleFor(c => c.Document).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);


    }
}