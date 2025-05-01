using FluentValidation;

namespace Application.Features.DefinitionFeatures.Departments.Commands.Create;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        
RuleFor(c => c.Name).NotNull().NotEmpty().MaximumLength(100);


    }
}