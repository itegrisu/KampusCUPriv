using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Create;

public class CreateReservationHotelStaffCommandValidator : AbstractValidator<CreateReservationHotelStaffCommand>
{
    public CreateReservationHotelStaffCommandValidator()
    {
        RuleFor(c => c.GidHotelFK).NotNull().NotEmpty();

        RuleFor(c => c.FullName).NotNull().NotEmpty().MaximumLength(60);
        RuleFor(c => c.GsmNo).MaximumLength(20);
        RuleFor(c => c.HotelStaffStatus).NotNull().NotEmpty();
        RuleFor(c => c.Password).MaximumLength(255);
        //RuleFor(c => c.PasswordHash).MaximumLength(255);


    }
}