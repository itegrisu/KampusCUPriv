using FluentValidation;

namespace Application.Features.DefinitionFeatures.AnnouncementTypes.Commands.Delete;

public class DeleteAnnouncementTypeCommandValidator : AbstractValidator<DeleteAnnouncementTypeCommand>
{
    public DeleteAnnouncementTypeCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}