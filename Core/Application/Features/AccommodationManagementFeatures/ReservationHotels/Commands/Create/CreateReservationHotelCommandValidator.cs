using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Create;

public class CreateReservationHotelCommandValidator : AbstractValidator<CreateReservationHotelCommand>
{
    public CreateReservationHotelCommandValidator()
    {
        RuleFor(c => c.GidReservationFK).NotNull().NotEmpty();
RuleFor(c => c.GidHotelFK).NotNull().NotEmpty();
RuleFor(c => c.GidBuyCurrencyTypeFK).NotNull().NotEmpty();
RuleFor(c => c.GidSellCurrencyTypeFK).NotNull().NotEmpty();



    }
}