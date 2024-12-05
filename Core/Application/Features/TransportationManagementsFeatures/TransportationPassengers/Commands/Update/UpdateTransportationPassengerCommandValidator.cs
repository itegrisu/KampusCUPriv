using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Update;

public class UpdateTransportationPassengerCommandValidator : AbstractValidator<UpdateTransportationPassengerCommand>
{
    public UpdateTransportationPassengerCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();

        RuleFor(c => c.Country).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.IdentityNo).NotNull().NotEmpty().MaximumLength(30);
        RuleFor(c => c.FirstName).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.LastName).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Gender).NotNull().NotEmpty();
        RuleFor(c => c.Phone).MaximumLength(20);
        RuleFor(c => c.PassengerStatus).NotNull().NotEmpty();
        RuleFor(c => c.RefNoTransportationPassenger).MaximumLength(50);
    }
}