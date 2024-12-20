using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Create;

public class CreateReservationRoomCommandValidator : AbstractValidator<CreateReservationRoomCommand>
{
    public CreateReservationRoomCommandValidator()
    {
        RuleFor(c => c.GidReservationDetailFK).NotNull().NotEmpty();

RuleFor(c => c.RoomNo).NotNull().NotEmpty();


    }
}