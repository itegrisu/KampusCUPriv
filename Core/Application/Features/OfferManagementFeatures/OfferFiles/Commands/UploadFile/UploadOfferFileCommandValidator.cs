using FluentValidation;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Commands.UploadFile
{
    public class UploadOfferFileCommandValidator : AbstractValidator<UploadOfferFileCommand>
    {
        public UploadOfferFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
