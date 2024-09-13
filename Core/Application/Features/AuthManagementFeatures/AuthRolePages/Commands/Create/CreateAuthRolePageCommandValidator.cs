using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Create;

public class CreateAuthRolePageCommandValidator : AbstractValidator<CreateAuthRolePageCommand>
{
    public CreateAuthRolePageCommandValidator()
    {
        RuleFor(c => c.GidRoleFK).NotEmpty();
        RuleFor(c => c.GidPageFK).NotEmpty();
    }
}