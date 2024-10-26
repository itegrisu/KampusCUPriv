using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Commands.Create;

public class CreateUserReminderCommandValidator : AbstractValidator<CreateUserReminderCommand>
{
    public CreateUserReminderCommandValidator()
    {
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();

RuleFor(c => c.Date).NotNull().NotEmpty();
RuleFor(c => c.Title).NotNull().NotEmpty().MaximumLength(100);
RuleFor(c => c.Description).MaximumLength(250);
RuleFor(c => c.Document).MaximumLength(150);
RuleFor(c => c.ReminderType).NotNull().NotEmpty();


    }
}