using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Commands.Update;

public class UpdateTaskUserCommandValidator : AbstractValidator<UpdateTaskUserCommand>
{
    public UpdateTaskUserCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.TaskGid).NotEmpty();
        RuleFor(c => c.UserGid).NotEmpty();
        RuleFor(c => c.TaskState).NotEmpty();
        RuleFor(c => c.StatusNote).MaximumLength(250);
    }
}