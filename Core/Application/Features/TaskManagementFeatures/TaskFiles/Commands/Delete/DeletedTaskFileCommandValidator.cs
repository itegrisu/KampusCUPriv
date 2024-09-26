using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.Delete;

public class DeleteTaskFileCommandValidator : AbstractValidator<DeleteTaskFileCommand>
{
    public DeleteTaskFileCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
    }
}