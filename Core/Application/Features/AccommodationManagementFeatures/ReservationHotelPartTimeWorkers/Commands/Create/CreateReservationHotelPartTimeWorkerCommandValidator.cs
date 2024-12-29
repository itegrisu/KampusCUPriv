using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelPartTimeWorkers.Commands.Create;

public class CreateReservationHotelPartTimeWorkerCommandValidator : AbstractValidator<CreateReservationHotelPartTimeWorkerCommand>
{
    public CreateReservationHotelPartTimeWorkerCommandValidator()
    {
        RuleFor(c => c.GidHotelFK).NotNull().NotEmpty();
RuleFor(c => c.GidPartTimeWorkerFK).NotNull().NotEmpty();

RuleFor(c => c.IsActive).NotNull().NotEmpty();
RuleFor(c => c.Note).MaximumLength(100);


    }
}