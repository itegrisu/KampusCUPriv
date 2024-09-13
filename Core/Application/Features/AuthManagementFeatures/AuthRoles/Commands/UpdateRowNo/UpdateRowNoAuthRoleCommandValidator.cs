using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthRoles.Commands.UpdateRowNo;

public class UpdateRowNoAuthRoleCommandValidator : AbstractValidator<UpdateRowNoAuthRoleCommand>
{
    public UpdateRowNoAuthRoleCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.IsUp).NotNull().NotEmpty();
    }
}