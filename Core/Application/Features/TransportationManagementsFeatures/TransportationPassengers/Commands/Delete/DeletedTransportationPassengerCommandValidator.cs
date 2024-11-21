using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.TransportationPassengers.Commands.Delete;

public class DeleteTransportationPassengerCommandValidator : AbstractValidator<DeleteTransportationPassengerCommand>
{
    public DeleteTransportationPassengerCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}