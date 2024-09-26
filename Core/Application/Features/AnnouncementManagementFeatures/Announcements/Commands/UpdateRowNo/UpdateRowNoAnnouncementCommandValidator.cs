using FluentValidation;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.UpdateRowNo;

public class UpdateRowNoAnnouncementCommandValidator : AbstractValidator<UpdateRowNoAnnouncementCommand>
{
    public UpdateRowNoAnnouncementCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}