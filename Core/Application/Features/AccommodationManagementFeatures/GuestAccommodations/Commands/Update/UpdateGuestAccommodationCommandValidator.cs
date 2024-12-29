using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Update;

public class UpdateGuestAccommodationCommandValidator : AbstractValidator<UpdateGuestAccommodationCommand>
{
    public UpdateGuestAccommodationCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidHotelFK).NotNull().NotEmpty();
//RuleFor(c => c.GidBuyCurrencyFK);//
//RuleFor(c => c.GidSellCurrencyFK);//

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.Institution).MaximumLength(50);
RuleFor(c => c.GuestCount).NotNull().NotEmpty();
RuleFor(c => c.Description).MaximumLength(250);
RuleFor(c => c.GuestAccommodationStatus).NotNull().NotEmpty();


    }
}