using FluentValidation;

namespace Application.Features.CommunicationFeatures.Calendars.Commands.Create;

public class CreateCalendarCommandValidator : AbstractValidator<CreateCalendarCommand>
{
    public CreateCalendarCommandValidator()
    {
        RuleFor(c => c.GidEventFK).NotNull().NotEmpty();

RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(150);
RuleFor(c => c.Date).NotNull().NotEmpty();
RuleFor(c => c.Color).MaximumLength(7);


    }
}