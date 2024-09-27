using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Delete;

public class DeleteSCWorkHistoryCommandValidator : AbstractValidator<DeleteSCWorkHistoryCommand>
{
    public DeleteSCWorkHistoryCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}