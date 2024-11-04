using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Update;

public class UpdateSCPersonnelCommandValidator : AbstractValidator<UpdateSCPersonnelCommand>
{
    public UpdateSCPersonnelCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidSCCompanyFK).NotNull().NotEmpty();
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();
        RuleFor(c => c.SCPersonnelLoginStatus).NotNull().NotEmpty();


    }
}