using FluentValidation;

namespace Application.Features.CommunicationFeatures.Announcements.Commands.Delete;

public class DeleteAnnouncementCommandValidator : AbstractValidator<DeleteAnnouncementCommand>
{
    public DeleteAnnouncementCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}