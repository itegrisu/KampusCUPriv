using FluentValidation;

namespace Application.Features.AnnouncementManagementFeatures.AnnouncementRecipients.Commands.Create;

public class CreateAnnouncementRecipientCommandValidator : AbstractValidator<CreateAnnouncementRecipientCommand>
{
    public CreateAnnouncementRecipientCommandValidator()
    {
        RuleFor(c => c.GidAnnouncementFK).NotEmpty();
        //RuleFor(c => c.GidRecipientFK).NotEmpty();
        // RuleFor(c => c.ReadDate).NotEmpty();
        //RuleFor(c => c.ReadIpAddress).NotEmpty();
        // RuleFor(c => c.Confirm).NotEmpty();
    }
}