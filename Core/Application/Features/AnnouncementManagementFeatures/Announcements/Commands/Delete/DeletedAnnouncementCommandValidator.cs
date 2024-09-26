using FluentValidation;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Delete;

public class DeleteAnnouncementCommandValidator : AbstractValidator<DeleteAnnouncementCommand>
{
    public DeleteAnnouncementCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}