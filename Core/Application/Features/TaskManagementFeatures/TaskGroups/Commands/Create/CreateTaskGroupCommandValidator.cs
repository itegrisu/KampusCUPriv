using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskGroups.Commands.Create;

public class CreateTaskGroupCommandValidator : AbstractValidator<CreateTaskGroupCommand>
{
    public CreateTaskGroupCommandValidator()
    {
        RuleFor(c => c.GroupName).NotEmpty().MaximumLength(100);
    }
}