using FluentValidation;

namespace Application.Features.OfferManagementFeatures.Offers.Commands.Create;

public class CreateOfferCommandValidator : AbstractValidator<CreateOfferCommand>
{
    public CreateOfferCommandValidator()
    {
        
RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(150);
RuleFor(c => c.Customer).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.OfferStatus).NotNull().NotEmpty();


    }
}