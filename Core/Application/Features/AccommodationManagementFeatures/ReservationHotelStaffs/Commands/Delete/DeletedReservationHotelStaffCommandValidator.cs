using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Delete;

public class DeleteReservationHotelStaffCommandValidator : AbstractValidator<DeleteReservationHotelStaffCommand>
{
    public DeleteReservationHotelStaffCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}