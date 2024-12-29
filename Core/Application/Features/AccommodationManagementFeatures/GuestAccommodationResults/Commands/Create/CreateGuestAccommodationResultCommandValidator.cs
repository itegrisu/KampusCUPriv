using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Create;

public class CreateGuestAccommodationResultCommandValidator : AbstractValidator<CreateGuestAccommodationResultCommand>
{
    public CreateGuestAccommodationResultCommandValidator()
    {
        RuleFor(c => c.GidGuestAccommodationPersonFK).NotNull().NotEmpty();
RuleFor(c => c.GidGuestAccommodationRoomFK).NotNull().NotEmpty();

RuleFor(c => c.Note).MaximumLength(100);


    }
}