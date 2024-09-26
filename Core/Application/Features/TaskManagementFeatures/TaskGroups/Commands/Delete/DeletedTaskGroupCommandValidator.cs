using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Commands.Delete;

public class DeleteTaskGroupCommandValidator : AbstractValidator<DeleteTaskGroupCommand>
{
    public DeleteTaskGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}