using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Departments.Commands.Update;

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidMainAdminFK).NotNull().NotEmpty();
        //RuleFor(c => c.GidCoAdminFK);//
        RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Details).MaximumLength(250);


    }
}