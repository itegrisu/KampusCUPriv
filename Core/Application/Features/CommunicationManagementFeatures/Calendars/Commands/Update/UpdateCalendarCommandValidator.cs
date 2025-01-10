using FluentValidation;

namespace Application.Features.CommunicationFeatures.Calendars.Commands.Update;

public class UpdateCalendarCommandValidator : AbstractValidator<UpdateCalendarCommand>
{
    public UpdateCalendarCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidEventFK).NotNull().NotEmpty();

RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(150);
RuleFor(c => c.Date).NotNull().NotEmpty();
RuleFor(c => c.Color).MaximumLength(7);


    }
}