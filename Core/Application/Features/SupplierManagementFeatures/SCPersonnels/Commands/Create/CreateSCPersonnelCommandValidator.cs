using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Create;

public class CreateSCPersonnelCommandValidator : AbstractValidator<CreateSCPersonnelCommand>
{
    public CreateSCPersonnelCommandValidator()
    {
        RuleFor(c => c.GidSCCompanyFK).NotNull().NotEmpty();
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();

        RuleFor(c => c.SCPersonnelLoginStatus).NotNull().NotEmpty();


    }
}