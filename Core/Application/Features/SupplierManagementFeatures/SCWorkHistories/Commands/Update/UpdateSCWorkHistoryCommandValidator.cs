using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Update;

public class UpdateSCWorkHistoryCommandValidator : AbstractValidator<UpdateSCWorkHistoryCommand>
{
    public UpdateSCWorkHistoryCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidSCCompanyFK).NotNull().NotEmpty();
        RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Detail).MaximumLength(250);
        RuleFor(c => c.WorkFile).MaximumLength(150);

    }
}