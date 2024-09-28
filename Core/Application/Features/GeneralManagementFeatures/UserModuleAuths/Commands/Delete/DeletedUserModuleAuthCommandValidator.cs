using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Delete;

public class DeleteUserModuleAuthCommandValidator : AbstractValidator<DeleteUserModuleAuthCommand>
{
    public DeleteUserModuleAuthCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}