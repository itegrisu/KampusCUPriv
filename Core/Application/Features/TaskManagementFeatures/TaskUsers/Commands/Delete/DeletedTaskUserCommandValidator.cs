using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Commands.Delete;

public class DeleteTaskUserCommandValidator : AbstractValidator<DeleteTaskUserCommand>
{
    public DeleteTaskUserCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}