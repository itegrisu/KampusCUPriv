using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Update;

public class UpdateReservationDetailCommandValidator : AbstractValidator<UpdateReservationDetailCommand>
{
    public UpdateReservationDetailCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidReservationHotelFK).NotNull().NotEmpty();
        RuleFor(c => c.GidRoomTypeFK).NotNull().NotEmpty();
        RuleFor(c => c.ReservationDate).NotNull().NotEmpty();
        RuleFor(c => c.RoomCount).NotNull().NotEmpty();
    }
}