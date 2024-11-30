using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.TransportationServices.Commands.Update;

public class UpdateTransportationServiceCommandValidator : AbstractValidator<UpdateTransportationServiceCommand>
{
    public UpdateTransportationServiceCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
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