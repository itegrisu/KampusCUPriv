using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Commands.Update;

public class UpdateTaskGroupCommandValidator : AbstractValidator<UpdateTaskGroupCommand>
{
    public UpdateTaskGroupCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.GroupName).NotEmpty().MaximumLength(100);
    }
}