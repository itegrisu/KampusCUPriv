using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Commands.Create;

public class CreatePersonnelAddressCommandValidator : AbstractValidator<CreatePersonnelAddressCommand>
{
    public CreatePersonnelAddressCommandValidator()
    {
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();
        RuleFor(c => c.GidCityFK).NotNull().NotEmpty();

        RuleFor(c => c.AddressTitle).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Address).NotNull().NotEmpty().MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);


    }
}