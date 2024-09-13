using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Departments.Commands.Update;

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidAsilYoneticiFK).NotNull().NotEmpty();
        //RuleFor(c => c.GidYedekYoneticiFK);//

        RuleFor(c => c.DepartmanAdi).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(c => c.Detay).MaximumLength(250);


    }
}