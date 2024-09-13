using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Commands.Update;

public class UpdateAuthRoleCommandValidator : AbstractValidator<UpdateAuthRoleCommand>
{
    public UpdateAuthRoleCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.RoleName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.RoleDescription).MaximumLength(250);
        RuleFor(c => c.IconImage).MaximumLength(100);
    }
}