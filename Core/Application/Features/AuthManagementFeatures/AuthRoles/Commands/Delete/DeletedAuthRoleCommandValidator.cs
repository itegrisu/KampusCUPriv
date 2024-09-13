using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Commands.Delete;

public class DeleteAuthRoleCommandValidator : AbstractValidator<DeleteAuthRoleCommand>
{
    public DeleteAuthRoleCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}