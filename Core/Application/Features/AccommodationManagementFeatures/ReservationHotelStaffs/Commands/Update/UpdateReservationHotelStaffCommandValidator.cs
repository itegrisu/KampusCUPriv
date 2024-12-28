using FluentValidation;

namespace Application.Features.AccommodationManagementFeatures.ReservationHotelStaffs.Commands.Update;

public class UpdateReservationHotelStaffCommandValidator : AbstractValidator<UpdateReservationHotelStaffCommand>
{
    public UpdateReservationHotelStaffCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidHotelFK).NotNull().NotEmpty();

        RuleFor(c => c.FullName).NotNull().NotEmpty().MaximumLength(60);
        RuleFor(c => c.GsmNo).MaximumLength(20);
        RuleFor(c => c.HotelStaffStatus).NotNull().NotEmpty();
        RuleFor(c => c.Password).MaximumLength(255);
        //RuleFor(c => c.PasswordHash).MaximumLength(255);


    }
}