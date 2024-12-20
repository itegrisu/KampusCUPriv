using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationRooms.Commands.Delete;

public class DeleteReservationRoomCommandValidator : AbstractValidator<DeleteReservationRoomCommand>
{
    public DeleteReservationRoomCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}