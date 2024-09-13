using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Delete;

public class DeletePersonnelGraduatedSchoolCommandValidator : AbstractValidator<DeletePersonnelGraduatedSchoolCommand>
{
    public DeletePersonnelGraduatedSchoolCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}