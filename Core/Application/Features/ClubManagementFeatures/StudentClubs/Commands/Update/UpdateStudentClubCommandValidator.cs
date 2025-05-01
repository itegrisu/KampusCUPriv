using FluentValidation;

namespace Application.Features.ClubFeatures.StudentClubs.Commands.Update;

public class UpdateStudentClubCommandValidator : AbstractValidator<UpdateStudentClubCommand>
{
    public UpdateStudentClubCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();
RuleFor(c => c.GidClubFK).NotNull().NotEmpty();



    }
}