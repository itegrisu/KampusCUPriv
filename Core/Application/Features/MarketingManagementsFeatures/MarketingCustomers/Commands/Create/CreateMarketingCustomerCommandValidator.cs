using FluentValidation;

namespace Application.Features.MarketingManagementFeatures.MarketingCustomers.Commands.Create;

public class CreateMarketingCustomerCommandValidator : AbstractValidator<CreateMarketingCustomerCommand>
{
    public CreateMarketingCustomerCommandValidator()
    {
        
RuleFor(c => c.FullName).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Company).MaximumLength(100);
RuleFor(c => c.Duty).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.PreviousDuty).MaximumLength(250);
RuleFor(c => c.Gsm).MaximumLength(20);
RuleFor(c => c.Email).MaximumLength(100);
RuleFor(c => c.Description).MaximumLength(250);


    }
}