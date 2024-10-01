using FluentValidation;

namespace Application.Features.OfferManagementFeatures.Offers.Commands.Update;

public class UpdateOfferCommandValidator : AbstractValidator<UpdateOfferCommand>
{
    public UpdateOfferCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(150);
RuleFor(c => c.Customer).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.OfferStatus).NotNull().NotEmpty();


    }
}