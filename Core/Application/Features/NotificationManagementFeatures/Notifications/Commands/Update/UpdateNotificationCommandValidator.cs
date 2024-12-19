using FluentValidation;

namespace Application.Features.NotificationManagementFeatures.Notifications.Commands.Update;

public class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
{
    public UpdateNotificationCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.GidUserFK).NotEmpty();
        RuleFor(c => c.Title).NotEmpty().MaximumLength(100);
        RuleFor(c => c.ProcessType).NotEmpty();
        //RuleFor(c => c.ReadingDate).NotEmpty();
        RuleFor(c => c.ReadingIp).NotEmpty().MaximumLength(50);
        RuleFor(c => c.Content).NotEmpty().MaximumLength(250);
    }
}