using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.UpdateRowNo;

public class UpdateRowNoAuthUserRoleCommandValidator : AbstractValidator<UpdateRowNoAuthUserRoleCommand>
{
    public UpdateRowNoAuthUserRoleCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}