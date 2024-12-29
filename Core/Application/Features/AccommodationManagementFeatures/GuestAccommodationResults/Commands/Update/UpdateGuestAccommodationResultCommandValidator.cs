using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.GuestAccommodationResults.Commands.Update;

public class UpdateGuestAccommodationResultCommandValidator : AbstractValidator<UpdateGuestAccommodationResultCommand>
{
    public UpdateGuestAccommodationResultCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidGuestAccommodationPersonFK).NotNull().NotEmpty();
RuleFor(c => c.GidGuestAccommodationRoomFK).NotNull().NotEmpty();

RuleFor(c => c.Note).MaximumLength(100);


    }
}