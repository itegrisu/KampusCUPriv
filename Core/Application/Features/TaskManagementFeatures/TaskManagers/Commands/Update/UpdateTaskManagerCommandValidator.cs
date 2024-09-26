using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Commands.Update;

public class UpdateTaskManagerCommandValidator : AbstractValidator<UpdateTaskManagerCommand>
{
    public UpdateTaskManagerCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.GidUserFK).NotEmpty();
    }
}