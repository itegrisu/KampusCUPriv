using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Update;

public class UpdateAuthUserRoleCommandValidator : AbstractValidator<UpdateAuthUserRoleCommand>
{
    public UpdateAuthUserRoleCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.GidUserFK).NotEmpty();
        //RuleFor(c => c.GidRoleFK).NotEmpty();
        //RuleFor(c => c.GidPageFK).NotEmpty();
        RuleFor(c => c.RowNo).NotEmpty();
    }
}