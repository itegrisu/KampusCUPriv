using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Commands.Delete;

public class DeleteUserReminderCommandValidator : AbstractValidator<DeleteUserReminderCommand>
{
    public DeleteUserReminderCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}