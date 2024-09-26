using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskComments.Commands.Update;

public class UpdateTaskCommentCommandValidator : AbstractValidator<UpdateTaskCommentCommand>
{
    public UpdateTaskCommentCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.GidUserFK).NotEmpty();
        RuleFor(c => c.GidTaskFK).NotEmpty();
        RuleFor(c => c.Comment).NotEmpty().MaximumLength(250);
    }
}