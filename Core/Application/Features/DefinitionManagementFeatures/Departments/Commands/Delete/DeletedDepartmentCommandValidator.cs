using FluentValidation;

namespace Application.Features.DefinitionFeatures.Departments.Commands.Delete;

public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
{
    public DeleteDepartmentCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}