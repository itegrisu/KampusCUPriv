using FluentValidation;

namespace Application.Features.TaskManagementFeatures.Tasks.Commands.Delete;

public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
{
    public DeleteTaskCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}