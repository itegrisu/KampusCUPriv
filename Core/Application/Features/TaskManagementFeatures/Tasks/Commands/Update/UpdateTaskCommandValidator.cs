using FluentValidation;

namespace Application.Features.TaskManagementFeatures.Tasks.Commands.Update;

public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
{
    public UpdateTaskCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.TaskAssignerUserGid).NotEmpty();
        RuleFor(c => c.Title).NotEmpty().MaximumLength(150);
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.Description).NotEmpty().MaximumLength(500);
        RuleFor(c => c.PriorityType).NotEmpty();
    }
}