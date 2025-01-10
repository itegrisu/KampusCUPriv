using FluentValidation;

namespace Application.Features.CommunicationFeatures.Events.Commands.Delete;

public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
{
    public DeleteEventCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}