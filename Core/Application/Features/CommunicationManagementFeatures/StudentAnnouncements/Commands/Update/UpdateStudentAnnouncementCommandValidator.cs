using FluentValidation;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Update;

public class UpdateStudentAnnouncementCommandValidator : AbstractValidator<UpdateStudentAnnouncementCommand>
{
    public UpdateStudentAnnouncementCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();
RuleFor(c => c.GidAnnouncementFK).NotNull().NotEmpty();

RuleFor(c => c.IsRead).NotNull().NotEmpty();


    }
}