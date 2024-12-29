using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Update;

public class UpdateReservationHotelPartTimeWorkerCommandValidator : AbstractValidator<UpdateReservationHotelPartTimeWorkerCommand>
{
    public UpdateReservationHotelPartTimeWorkerCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidHotelFK).NotNull().NotEmpty();
RuleFor(c => c.GidPartTimeWorkerFK).NotNull().NotEmpty();

RuleFor(c => c.IsActive).NotNull().NotEmpty();
RuleFor(c => c.Note).MaximumLength(100);


    }
}