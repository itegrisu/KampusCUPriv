using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Create;

public class CreateSCEmployerCommandValidator : AbstractValidator<CreateSCEmployerCommand>
{
    public CreateSCEmployerCommandValidator()
    {
        RuleFor(c => c.GidSCCompanyFK).NotNull().NotEmpty();

        RuleFor(c => c.FullName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Duty).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Phone).MaximumLength(20);
        RuleFor(c => c.Email).MaximumLength(100);
        RuleFor(c => c.SpecialNote).MaximumLength(250);


    }
}