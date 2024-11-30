using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Create;

public class CreateTransportationServiceCommandValidator : AbstractValidator<CreateTransportationServiceCommand>
{
    public CreateTransportationServiceCommandValidator()
    {
        RuleFor(c => c.GidTransportationFK).NotNull().NotEmpty();
        RuleFor(c => c.GidVehicleFK).NotNull().NotEmpty();

        RuleFor(c => c.ServiceNo).NotNull().NotEmpty().MaximumLength(20);
        RuleFor(c => c.StartDate).NotNull().NotEmpty();
        RuleFor(c => c.EndDate).NotNull().NotEmpty();
        RuleFor(c => c.VehiclePhone).MaximumLength(20);
        RuleFor(c => c.TransportationServiceStatus).NotNull().NotEmpty();
        RuleFor(c => c.TransportationFile).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);


    }
}