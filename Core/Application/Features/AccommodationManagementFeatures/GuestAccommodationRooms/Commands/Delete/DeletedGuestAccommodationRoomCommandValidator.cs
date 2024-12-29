using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Delete;

public class DeleteGuestAccommodationRoomCommandValidator : AbstractValidator<DeleteGuestAccommodationRoomCommand>
{
    public DeleteGuestAccommodationRoomCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}