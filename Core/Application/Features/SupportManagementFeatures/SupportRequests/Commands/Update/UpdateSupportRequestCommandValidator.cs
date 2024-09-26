using FluentValidation;

namespace Application.Features.SupportManagementFeatures.SupportRequests.Commands.Update;

public class UpdateSupportRequestCommandValidator : AbstractValidator<UpdateSupportRequestCommand>
{
    public UpdateSupportRequestCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.SupportStatus).NotNull().NotEmpty();
        RuleFor(c => c.PriorityType).NotNull().NotEmpty();
        RuleFor(c => c.SupportType).NotNull().NotEmpty();


    }
}