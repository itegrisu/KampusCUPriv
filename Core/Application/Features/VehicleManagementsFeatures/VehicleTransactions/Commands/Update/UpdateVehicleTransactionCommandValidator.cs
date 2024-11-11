using FluentValidation;

namespace Application.Features.VehicleManagementFeatures.VehicleTransactions.Commands.Update;

public class UpdateVehicleTransactionCommandValidator : AbstractValidator<UpdateVehicleTransactionCommand>
{
    public UpdateVehicleTransactionCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        //RuleFor(c => c.GidSupplierCustomerFK);//
        //RuleFor(c => c.GidVehicleUsePersonnelFK);//

        RuleFor(c => c.StartKM).NotNull().NotEmpty();
        RuleFor(c => c.ContactPerson).MaximumLength(60);
        RuleFor(c => c.ContactPhone).MaximumLength(20);
        RuleFor(c => c.ArventoAPIInfo).MaximumLength(250);
        RuleFor(c => c.LicenseFile).MaximumLength(150);
        RuleFor(c => c.ContractFile).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);
        RuleFor(c => c.VehicleStatus).NotNull().NotEmpty();


    }
}