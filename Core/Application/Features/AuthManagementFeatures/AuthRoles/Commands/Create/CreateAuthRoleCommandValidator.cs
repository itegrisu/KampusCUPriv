using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Commands.Create;

public class CreateAuthRoleCommandValidator : AbstractValidator<CreateAuthRoleCommand>
{
    public CreateAuthRoleCommandValidator()
    {
        RuleFor(c => c.RoleName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.RoleDescription).MaximumLength(250);
        RuleFor(c => c.IconImage).MaximumLength(100);
    }
}