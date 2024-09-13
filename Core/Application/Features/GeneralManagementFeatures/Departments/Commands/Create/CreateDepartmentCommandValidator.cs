using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Departments.Commands.Create;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(c => c.GidAsilYoneticiFK).NotNull().NotEmpty();
//RuleFor(c => c.GidYedekYoneticiFK);//

RuleFor(c => c.DepartmanAdi).NotNull().NotEmpty().MaximumLength(50);
RuleFor(c => c.Detay).MaximumLength(250);


    }
}