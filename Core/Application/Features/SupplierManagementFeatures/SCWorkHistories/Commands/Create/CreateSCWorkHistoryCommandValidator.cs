using FluentValidation;

namespace Application.Features.SupplierCustomerManagementFeatures.SCWorkHistories.Commands.Create;

public class CreateSCWorkHistoryCommandValidator : AbstractValidator<CreateSCWorkHistoryCommand>
{
    public CreateSCWorkHistoryCommandValidator()
    {
        RuleFor(c => c.GidSCCompanyFK).NotNull().NotEmpty();
        RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.Detail).MaximumLength(250);
        RuleFor(c => c.WorkFile).MaximumLength(150);
    }
}