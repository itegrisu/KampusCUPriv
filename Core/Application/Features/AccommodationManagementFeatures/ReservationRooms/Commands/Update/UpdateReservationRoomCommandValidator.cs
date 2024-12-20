using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Update;

public class UpdateReservationRoomCommandValidator : AbstractValidator<UpdateReservationRoomCommand>
{
    public UpdateReservationRoomCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidReservationDetailFK).NotNull().NotEmpty();

RuleFor(c => c.RoomNo).NotNull().NotEmpty();


    }
}