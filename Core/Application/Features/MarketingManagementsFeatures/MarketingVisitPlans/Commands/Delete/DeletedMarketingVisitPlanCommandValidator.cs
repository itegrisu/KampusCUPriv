using FluentValidation;

namespace Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Delete;

public class DeleteMarketingVisitPlanCommandValidator : AbstractValidator<DeleteMarketingVisitPlanCommand>
{
    public DeleteMarketingVisitPlanCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}