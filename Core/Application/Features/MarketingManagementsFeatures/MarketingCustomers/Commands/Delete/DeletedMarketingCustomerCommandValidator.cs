using FluentValidation;

namespace Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Delete;

public class DeleteMarketingCustomerCommandValidator : AbstractValidator<DeleteMarketingCustomerCommand>
{
    public DeleteMarketingCustomerCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}