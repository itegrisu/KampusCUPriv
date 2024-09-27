using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCBanks.Commands.Delete;

public class DeleteSCBankCommandValidator : AbstractValidator<DeleteSCBankCommand>
{
    public DeleteSCBankCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}