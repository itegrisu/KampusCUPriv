using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationPersons.Commands.Delete;

public class DeleteGuestAccommodationPersonCommandValidator : AbstractValidator<DeleteGuestAccommodationPersonCommand>
{
    public DeleteGuestAccommodationPersonCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}