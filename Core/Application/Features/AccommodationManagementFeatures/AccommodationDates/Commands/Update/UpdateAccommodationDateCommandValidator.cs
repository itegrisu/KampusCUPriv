using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.AccommodationDates.Commands.Update;

public class UpdateAccommodationDateCommandValidator : AbstractValidator<UpdateAccommodationDateCommand>
{
    public UpdateAccommodationDateCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidReservationDetailFK).NotNull().NotEmpty();
        //RuleFor(c => c.GidGuestFK);//
        //RuleFor(c => c.GidRoomNoFK);//

        RuleFor(c => c.Date).NotNull().NotEmpty();
        RuleFor(c => c.PreviousRoomInfo).MaximumLength(250);


    }
}