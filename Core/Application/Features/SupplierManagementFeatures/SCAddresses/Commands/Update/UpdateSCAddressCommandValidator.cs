using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Update;

public class UpdateSCAddressCommandValidator : AbstractValidator<UpdateSCAddressCommand>
{
    public UpdateSCAddressCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidSCCompanyFK).NotNull().NotEmpty();
RuleFor(c => c.GidCityFK).NotNull().NotEmpty();

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.District).MaximumLength(50);
RuleFor(c => c.PostalCode).MaximumLength(20);
RuleFor(c => c.Address).NotNull().NotEmpty().MaximumLength(250);


    }
}