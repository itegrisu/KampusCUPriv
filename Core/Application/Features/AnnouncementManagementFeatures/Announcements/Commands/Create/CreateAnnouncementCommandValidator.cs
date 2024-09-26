using FluentValidation;

namespace Application.Features.AnnouncementManagementFeatures.Announcements.Commands.Create;

public class CreateAnnouncementCommandValidator : AbstractValidator<CreateAnnouncementCommand>
{
    public CreateAnnouncementCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty().MaximumLength(150).MinimumLength(2);
        RuleFor(c => c.Description).NotEmpty().MaximumLength(1000).MinimumLength(2);
        RuleFor(c => c.Link).MaximumLength(250);
        RuleFor(c => c.Image).MaximumLength(150);
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.Status).NotEmpty();
        RuleFor(c => c.ShowType).NotEmpty();
        RuleFor(c => c.RowNo).NotEmpty();
    }
}