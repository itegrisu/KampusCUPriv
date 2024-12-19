using FluentValidation;

namespace Application.Features.NotificationManagementFeatures.Notifications.Commands.Delete;

public class DeleteNotificationCommandValidator : AbstractValidator<DeleteNotificationCommand>
{
    public DeleteNotificationCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}