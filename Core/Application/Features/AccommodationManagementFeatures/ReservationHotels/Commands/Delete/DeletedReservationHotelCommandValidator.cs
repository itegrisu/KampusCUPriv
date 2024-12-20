using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotels.Commands.Delete;

public class DeleteReservationHotelCommandValidator : AbstractValidator<DeleteReservationHotelCommand>
{
    public DeleteReservationHotelCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}