using FluentValidation;

namespace Application.Features.NotificationManagementFeatures.Notifications.Commands.MarkTheReadByGid;

public class MarkTheReadByGidCommandValidator : AbstractValidator<MarkTheReadByGidCommand>
{
    public MarkTheReadByGidCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}