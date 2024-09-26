using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Commands.Delete;

public class DeleteTaskManagerCommandValidator : AbstractValidator<DeleteTaskManagerCommand>
{
    public DeleteTaskManagerCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}