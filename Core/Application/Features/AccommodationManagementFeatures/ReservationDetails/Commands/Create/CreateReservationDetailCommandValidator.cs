using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Create;

public class CreateReservationDetailCommandValidator : AbstractValidator<CreateReservationDetailCommand>
{
    public CreateReservationDetailCommandValidator()
    {
        RuleFor(c => c.GidReservationHotelFK).NotNull().NotEmpty();
        RuleFor(c => c.GidRoomTypeFK).NotNull().NotEmpty();
        RuleFor(c => c.ReservationDate).NotNull().NotEmpty();
        RuleFor(c => c.RoomCount).NotNull().NotEmpty();
    }
}