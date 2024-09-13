using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Delete;

public class DeleteAuthUserRoleCommandValidator : AbstractValidator<DeleteAuthUserRoleCommand>
{
    public DeleteAuthUserRoleCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}