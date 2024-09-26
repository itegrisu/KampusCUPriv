using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Update;

public class UpdateTaskGroupUserCommandValidator : AbstractValidator<UpdateTaskGroupUserCommand>
{
    public UpdateTaskGroupUserCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.GidTaskGroupFK).NotEmpty();
        RuleFor(c => c.GidUserFK).NotEmpty();
    }
}