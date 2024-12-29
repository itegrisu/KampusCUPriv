using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Delete;

public class DeleteReservationHotelPartTimeWorkerCommandValidator : AbstractValidator<DeleteReservationHotelPartTimeWorkerCommand>
{
    public DeleteReservationHotelPartTimeWorkerCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}