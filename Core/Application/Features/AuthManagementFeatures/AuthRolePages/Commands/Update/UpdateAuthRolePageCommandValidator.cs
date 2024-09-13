using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Update;

public class UpdateAuthRolePageCommandValidator : AbstractValidator<UpdateAuthRolePageCommand>
{
    public UpdateAuthRolePageCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.GidRoleFK).NotEmpty();
        RuleFor(c => c.GidPageFK).NotEmpty();
    }
}