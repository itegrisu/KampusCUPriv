using FluentValidation;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Create;

public class CreateStudentAnnouncementCommandValidator : AbstractValidator<CreateStudentAnnouncementCommand>
{
    public CreateStudentAnnouncementCommandValidator()
    {
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();
RuleFor(c => c.GidAnnouncementFK).NotNull().NotEmpty();

RuleFor(c => c.IsRead).NotNull().NotEmpty();


    }
}