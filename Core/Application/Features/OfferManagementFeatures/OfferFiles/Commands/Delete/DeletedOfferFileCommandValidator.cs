using FluentValidation;

namespace Application.Features.OfferManagementFeatures.OfferFiles.Commands.Delete;

public class DeleteOfferFileCommandValidator : AbstractValidator<DeleteOfferFileCommand>
{
    public DeleteOfferFileCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}