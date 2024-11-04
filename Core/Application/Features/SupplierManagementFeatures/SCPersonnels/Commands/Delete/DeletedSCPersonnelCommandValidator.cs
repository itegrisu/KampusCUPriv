using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCPersonnels.Commands.Delete;

public class DeleteSCPersonnelCommandValidator : AbstractValidator<DeleteSCPersonnelCommand>
{
    public DeleteSCPersonnelCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}