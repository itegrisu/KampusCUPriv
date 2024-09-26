using FluentValidation;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Update;

public class UpdateAnnouncementRecipientCommandValidator : AbstractValidator<UpdateAnnouncementRecipientCommand>
{
    public UpdateAnnouncementRecipientCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.GidAnnouncementFK).NotEmpty();
        RuleFor(c => c.GidRecipientFK).NotEmpty();
        //RuleFor(c => c.ReadDate).NotEmpty();
        //RuleFor(c => c.ReadIpAddress).NotEmpty();
        //RuleFor(c => c.Confirm).NotEmpty();
    }
}