using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCCompanies.Commands.Delete;

public class DeleteSCCompanyCommandValidator : AbstractValidator<DeleteSCCompanyCommand>
{
    public DeleteSCCompanyCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}