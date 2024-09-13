using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Commands.Create;

public class CreateAuthUserRoleCommandValidator : AbstractValidator<CreateAuthUserRoleCommand>
{
    public CreateAuthUserRoleCommandValidator()
    {
        RuleFor(c => c.GidUserFK).NotEmpty();
        //  RuleFor(c => c.GidRoleFK).NotEmpty();
        //RuleFor(c => c.GidPageFK).NotEmpty();
    }
}