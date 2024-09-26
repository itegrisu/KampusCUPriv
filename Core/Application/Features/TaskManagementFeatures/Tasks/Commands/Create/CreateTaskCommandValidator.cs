using FluentValidation;

namespace Application.Features.TaskManagementFeatures.Tasks.Commands.Create;

public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(c => c.TaskAssignerUserGid).NotEmpty();
        RuleFor(c => c.Title).NotEmpty().MaximumLength(150);
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.Description).NotEmpty().MaximumLength(500);
        RuleFor(c => c.PriorityType).NotEmpty();
    }
}