using FluentValidation;

namespace Application.Features.DefinitionFeatures.Departments.Commands.Update;

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);


    }
}