using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Create;

public class CreateAccommodationDateCommandValidator : AbstractValidator<CreateAccommodationDateCommand>
{
    public CreateAccommodationDateCommandValidator()
    {
        RuleFor(c => c.GidReservationDetailFK).NotNull().NotEmpty();
        //RuleFor(c => c.GidGuestFK);//
        //RuleFor(c => c.GidRoomNoFK);//

        RuleFor(c => c.Date).NotNull().NotEmpty();
        RuleFor(c => c.PreviousRoomInfo).MaximumLength(250);
    }
}