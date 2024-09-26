using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Delete;

public class DeleteTaskGroupUserCommandValidator : AbstractValidator<DeleteTaskGroupUserCommand>
{
    public DeleteTaskGroupUserCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}