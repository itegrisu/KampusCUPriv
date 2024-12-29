using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Update;

public class UpdateGuestAccommodationRoomCommandValidator : AbstractValidator<UpdateGuestAccommodationRoomCommand>
{
    public UpdateGuestAccommodationRoomCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidGuestAccommodationFK).NotNull().NotEmpty();
RuleFor(c => c.GidRoomTypeFK).NotNull().NotEmpty();

RuleFor(c => c.Date).NotNull().NotEmpty();


    }
}