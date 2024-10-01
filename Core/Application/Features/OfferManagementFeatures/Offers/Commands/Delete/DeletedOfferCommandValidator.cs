using FluentValidation;

namespace Application.Features.OfferManagementFeatures.Offers.Commands.Delete;

public class DeleteOfferCommandValidator : AbstractValidator<DeleteOfferCommand>
{
    public DeleteOfferCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}