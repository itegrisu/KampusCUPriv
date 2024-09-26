using Application.Features.GeneralManagementFeatures.Users.Commands.UploadAvatar;
using FluentValidation;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.UploadAnnouncementFile;

public class UploadAnouncementCommandValidator : AbstractValidator<UploadAnnouncementFileCommand>
{
    public UploadAnouncementCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();

    }
}