using FluentValidation;

namespace Application.Features.MarketingManagementFeatures.MarketingVisitPlans.Commands.Create;

public class CreateMarketingVisitPlanCommandValidator : AbstractValidator<CreateMarketingVisitPlanCommand>
{
    public CreateMarketingVisitPlanCommandValidator()
    {
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();
RuleFor(c => c.GidVisitCustomerFK).NotNull().NotEmpty();

RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.PlanningVisitDate).NotNull().NotEmpty();
RuleFor(c => c.Description).MaximumLength(500);
RuleFor(c => c.VisitStatus).NotNull().NotEmpty();
RuleFor(c => c.VisitNote).MaximumLength(300);


    }
}