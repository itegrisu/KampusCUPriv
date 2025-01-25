using FluentValidation;

namespace Application.Features.CommunicationFeatures.StudentAnnouncements.Commands.Delete;

public class DeleteStudentAnnouncementCommandValidator : AbstractValidator<DeleteStudentAnnouncementCommand>
{
    public DeleteStudentAnnouncementCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}