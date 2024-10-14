using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Departments.Commands.Create;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(c => c.GidMainAdminFK).NotNull().NotEmpty();
        //RuleFor(c => c.GidCoAdminFK);//

        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Details).MaximumLength(250);


    }
}