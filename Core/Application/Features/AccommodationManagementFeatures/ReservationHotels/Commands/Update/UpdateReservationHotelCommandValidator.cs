using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Update;

public class UpdateReservationHotelCommandValidator : AbstractValidator<UpdateReservationHotelCommand>
{
    public UpdateReservationHotelCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidReservationFK).NotNull().NotEmpty();
RuleFor(c => c.GidHotelFK).NotNull().NotEmpty();
RuleFor(c => c.GidBuyCurrencyTypeFK).NotNull().NotEmpty();
RuleFor(c => c.GidSellCurrencyTypeFK).NotNull().NotEmpty();



    }
}