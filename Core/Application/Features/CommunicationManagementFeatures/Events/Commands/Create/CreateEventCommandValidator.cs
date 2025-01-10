using FluentValidation;

namespace Application.Features.CommunicationFeatures.Events.Commands.Create;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(c => c.GidClubFK).NotNull().NotEmpty();

RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(250);
RuleFor(c => c.StartDate).NotNull().NotEmpty();
RuleFor(c => c.EndDate).NotNull().NotEmpty();
RuleFor(c => c.Location).MaximumLength(300);
RuleFor(c => c.Description).MaximumLength(300);
RuleFor(c => c.EventStatus).NotNull().NotEmpty();


    }
}