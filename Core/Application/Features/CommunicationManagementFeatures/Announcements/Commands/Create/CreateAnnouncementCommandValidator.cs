using FluentValidation;

namespace Application.Features.CommunicationFeatures.Announcements.Commands.Create;

public class CreateAnnouncementCommandValidator : AbstractValidator<CreateAnnouncementCommand>
{
    public CreateAnnouncementCommandValidator()
    {
        //RuleFor(c => c.GidClubFK);//
        //RuleFor(c => c.GidUserFK);//

        RuleFor(c => c.Description).NotNull().NotEmpty().MaximumLength(300);


    }
}