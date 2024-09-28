using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Create;

public class CreateSCCompanyCommandValidator : AbstractValidator<CreateSCCompanyCommand>
{
    public CreateSCCompanyCommandValidator()
    {
        
RuleFor(c => c.CompanyName).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Phone).MaximumLength(20);
RuleFor(c => c.WebSite).MaximumLength(100);
RuleFor(c => c.Email).MaximumLength(100);
RuleFor(c => c.Password).MaximumLength(50);
RuleFor(c => c.WebLoginStatus).NotNull().NotEmpty();
RuleFor(c => c.Description).MaximumLength(250);
RuleFor(c => c.SpecialNote).MaximumLength(250);
RuleFor(c => c.TaxOffice).MaximumLength(100);
RuleFor(c => c.TaxNumber).MaximumLength(50);
RuleFor(c => c.Keywords).MaximumLength(300);
RuleFor(c => c.PartnerType).NotNull().NotEmpty();
RuleFor(c => c.Type).NotNull().NotEmpty();
RuleFor(c => c.Status).NotNull().NotEmpty();


    }
}