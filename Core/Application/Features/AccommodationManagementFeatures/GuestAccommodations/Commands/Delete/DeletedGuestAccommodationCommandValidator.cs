using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodations.Commands.Delete;

public class DeleteGuestAccommodationCommandValidator : AbstractValidator<DeleteGuestAccommodationCommand>
{
    public DeleteGuestAccommodationCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}