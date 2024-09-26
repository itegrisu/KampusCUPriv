using FluentValidation;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Delete;

public class DeleteAnnouncementRecipientCommandValidator : AbstractValidator<DeleteAnnouncementRecipientCommand>
{
    public DeleteAnnouncementRecipientCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}