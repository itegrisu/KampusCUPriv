using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}