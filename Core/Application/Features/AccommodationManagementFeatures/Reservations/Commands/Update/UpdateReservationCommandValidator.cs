using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.Reservations.Commands.Update;

public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
{
    public UpdateReservationCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        //RuleFor(c => c.GidOrganizationFK);//

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.StartDate).NotNull().NotEmpty();
RuleFor(c => c.EndDate).NotNull().NotEmpty();
RuleFor(c => c.ReservationType).NotNull().NotEmpty();
RuleFor(c => c.ReservationStatus).NotNull().NotEmpty();


    }
}