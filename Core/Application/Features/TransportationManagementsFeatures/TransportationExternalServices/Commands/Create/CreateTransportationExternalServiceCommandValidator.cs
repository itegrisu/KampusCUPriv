using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Create;

public class CreateTransportationExternalServiceCommandValidator : AbstractValidator<CreateTransportationExternalServiceCommand>
{
    public CreateTransportationExternalServiceCommandValidator()
    {
        RuleFor(c => c.GidSupplierFK).NotNull().NotEmpty();
//RuleFor(c => c.GidOrganizationFK);//
RuleFor(c => c.GidFeeCurrencyFK).NotNull().NotEmpty();

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Fee).NotNull().NotEmpty();
RuleFor(c => c.PlateNo).NotNull().NotEmpty().MaximumLength(20);
RuleFor(c => c.ExternalVehicleType).NotNull().NotEmpty();
RuleFor(c => c.VehicleOfficer).MaximumLength(100);
RuleFor(c => c.VehiclePhone).MaximumLength(20);
RuleFor(c => c.ExternalServiceStatus).NotNull().NotEmpty();
RuleFor(c => c.Description).MaximumLength(250);


    }
}