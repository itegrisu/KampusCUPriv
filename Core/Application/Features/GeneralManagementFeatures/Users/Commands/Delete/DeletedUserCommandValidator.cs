using FluentValidation;

namespace Application.Features.GeneralFeatures.Users.Commands.Delete;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}