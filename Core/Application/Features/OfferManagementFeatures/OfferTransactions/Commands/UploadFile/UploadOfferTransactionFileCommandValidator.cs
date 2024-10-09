using FluentValidation;

namespace Application.Features.OfferManagementFeatures.OfferTransactions.Commands.UploadFile
{
    public class UploadOfferTransactionFileCommandValidator : AbstractValidator<UploadOfferFileTransactionCommand>
    {
        public UploadOfferTransactionFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();

        }
    }
}
