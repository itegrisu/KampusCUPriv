using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.Guests.Commands.Delete;

public class DeleteGuestCommandValidator : AbstractValidator<DeleteGuestCommand>
{
    public DeleteGuestCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}