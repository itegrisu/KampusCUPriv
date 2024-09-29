using FluentValidation;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.Create;

public class CreatePersonnelGraduatedSchoolCommandValidator : AbstractValidator<CreatePersonnelGraduatedSchoolCommand>
{
    public CreatePersonnelGraduatedSchoolCommandValidator()
    {
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();

        RuleFor(c => c.EducationalInstitutionType).NotNull().NotEmpty();
        RuleFor(c => c.SchoolInfo).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.DepartmentInfo).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.StartYear).NotNull().NotEmpty();
        RuleFor(c => c.Document).MaximumLength(150);
        RuleFor(c => c.Description).MaximumLength(250);


    }
}