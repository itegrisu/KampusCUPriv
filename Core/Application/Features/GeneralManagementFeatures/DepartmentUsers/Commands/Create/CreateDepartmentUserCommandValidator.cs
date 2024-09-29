using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Create;

public class CreateDepartmentUserCommandValidator : AbstractValidator<CreateDepartmentUserCommand>
{
    public CreateDepartmentUserCommandValidator()
    {
        RuleFor(c => c.GidDepartmentFK).NotNull().NotEmpty();
        RuleFor(c => c.GidPersonnelFK).NotNull().NotEmpty();

    }
}