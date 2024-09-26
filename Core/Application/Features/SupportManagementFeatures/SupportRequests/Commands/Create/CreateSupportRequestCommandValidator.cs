using FluentValidation;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Commands.Create;

public class CreateSupportRequestCommandValidator : AbstractValidator<CreateSupportRequestCommand>
{
    public CreateSupportRequestCommandValidator()
    {
        
RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.SupportStatus).NotNull().NotEmpty();
RuleFor(c => c.PriorityType).NotNull().NotEmpty();
RuleFor(c => c.SupportType).NotNull().NotEmpty();


    }
}