using FluentValidation;

namespace Application.Features.SupportManagementFeatures.SupportMessages.Commands.Update;

public class UpdateSupportMessageCommandValidator : AbstractValidator<UpdateSupportMessageCommand>
{
    public UpdateSupportMessageCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidSupportFK).NotNull().NotEmpty();
RuleFor(c => c.GidSenderUserFK).NotNull().NotEmpty();

RuleFor(c => c.Message).NotNull().NotEmpty().MaximumLength(1000);
RuleFor(c => c.MessageType).NotNull().NotEmpty();


    }
}