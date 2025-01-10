using FluentValidation;

namespace Application.Features.CommunicationFeatures.Announcements.Commands.Update;

public class UpdateAnnouncementCommandValidator : AbstractValidator<UpdateAnnouncementCommand>
{
    public UpdateAnnouncementCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        //RuleFor(c => c.GidClubFK);//
//RuleFor(c => c.GidUserFK);//
RuleFor(c => c.GidAnnouncementType).NotNull().NotEmpty();

RuleFor(c => c.Description).NotNull().NotEmpty().MaximumLength(300);
RuleFor(c => c.IsRead).NotNull().NotEmpty();


    }
}