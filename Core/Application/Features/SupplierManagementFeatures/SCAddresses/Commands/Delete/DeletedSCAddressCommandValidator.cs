using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCAddresses.Commands.Delete;

public class DeleteSCAddressCommandValidator : AbstractValidator<DeleteSCAddressCommand>
{
    public DeleteSCAddressCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}