using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskComments.Commands.Delete;

public class DeleteTaskCommentCommandValidator : AbstractValidator<DeleteTaskCommentCommand>
{
    public DeleteTaskCommentCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
    }
}