using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.Reservations.Commands.Delete;

public class DeleteReservationCommandValidator : AbstractValidator<DeleteReservationCommand>
{
    public DeleteReservationCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}