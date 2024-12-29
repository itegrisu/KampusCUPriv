using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationRooms.Commands.Create;

public class CreateGuestAccommodationRoomCommandValidator : AbstractValidator<CreateGuestAccommodationRoomCommand>
{
    public CreateGuestAccommodationRoomCommandValidator()
    {
        RuleFor(c => c.GidGuestAccommodationFK).NotNull().NotEmpty();
RuleFor(c => c.GidRoomTypeFK).NotNull().NotEmpty();

RuleFor(c => c.Date).NotNull().NotEmpty();


    }
}