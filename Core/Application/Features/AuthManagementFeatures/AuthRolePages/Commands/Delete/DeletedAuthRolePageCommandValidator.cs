using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthRolePages.Commands.Delete;

public class DeleteAuthRolePageCommandValidator : AbstractValidator<DeleteAuthRolePageCommand>
{
    public DeleteAuthRolePageCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}