using FluentValidation;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Commands.Delete;

public class DeleteOfferTransactionCommandValidator : AbstractValidator<DeleteOfferTransactionCommand>
{
    public DeleteOfferTransactionCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}