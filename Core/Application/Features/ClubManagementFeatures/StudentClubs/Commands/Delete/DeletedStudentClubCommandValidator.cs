using FluentValidation;

namespace Application.Features.ClubFeatures.StudentClubs.Commands.Delete;

public class DeleteStudentClubCommandValidator : AbstractValidator<DeleteStudentClubCommand>
{
    public DeleteStudentClubCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}