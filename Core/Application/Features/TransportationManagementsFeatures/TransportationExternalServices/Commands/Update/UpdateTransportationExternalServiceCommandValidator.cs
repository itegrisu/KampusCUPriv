using FluentValidation;

namespace Application.Features.TransportationManagementFeatures.TransportationExternalServices.Commands.Update;

public class UpdateTransportationExternalServiceCommandValidator : AbstractValidator<UpdateTransportationExternalServiceCommand>
{
    public UpdateTransportationExternalServiceCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
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