using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCEmployers.Commands.Delete;

public class DeleteSCEmployerCommandValidator : AbstractValidator<DeleteSCEmployerCommand>
{
    public DeleteSCEmployerCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}