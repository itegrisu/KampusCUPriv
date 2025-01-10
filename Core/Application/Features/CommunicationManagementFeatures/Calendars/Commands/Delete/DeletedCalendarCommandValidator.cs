using FluentValidation;

namespace Application.Features.CommunicationFeatures.Calendars.Commands.Delete;

public class DeleteCalendarCommandValidator : AbstractValidator<DeleteCalendarCommand>
{
    public DeleteCalendarCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}