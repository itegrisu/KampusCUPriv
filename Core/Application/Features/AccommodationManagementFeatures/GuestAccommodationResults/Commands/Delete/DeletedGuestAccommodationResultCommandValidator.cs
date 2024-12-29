using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Delete;

public class DeleteGuestAccommodationResultCommandValidator : AbstractValidator<DeleteGuestAccommodationResultCommand>
{
    public DeleteGuestAccommodationResultCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}