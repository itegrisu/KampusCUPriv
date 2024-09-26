using FluentValidation;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.Create;

public class CreateSupportMessageCommandValidator : AbstractValidator<CreateSupportMessageCommand>
{
    public CreateSupportMessageCommandValidator()
    {
        RuleFor(c => c.GidSupportFK).NotNull().NotEmpty();
        RuleFor(c => c.GidSenderUserFK).NotNull().NotEmpty();
        RuleFor(c => c.Message).NotNull().NotEmpty().MaximumLength(1000);
    }
}