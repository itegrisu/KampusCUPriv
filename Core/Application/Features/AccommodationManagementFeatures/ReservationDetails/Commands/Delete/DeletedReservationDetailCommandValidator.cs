using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationDetails.Commands.Delete;

public class DeleteReservationDetailCommandValidator : AbstractValidator<DeleteReservationDetailCommand>
{
    public DeleteReservationDetailCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}