using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.DepartmentUsers.Commands.Delete;

public class DeleteDepartmentUserCommandValidator : AbstractValidator<DeleteDepartmentUserCommand>
{
    public DeleteDepartmentUserCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}