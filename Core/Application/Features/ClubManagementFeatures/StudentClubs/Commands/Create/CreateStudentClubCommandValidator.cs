using FluentValidation;

namespace Application.Features.ClubFeatures.StudentClubs.Commands.Create;

public class CreateStudentClubCommandValidator : AbstractValidator<CreateStudentClubCommand>
{
    public CreateStudentClubCommandValidator()
    {
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();
RuleFor(c => c.GidClubFK).NotNull().NotEmpty();



    }
}