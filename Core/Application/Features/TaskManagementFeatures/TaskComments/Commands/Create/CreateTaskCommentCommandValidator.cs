using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskComments.Commands.Create;

public class CreateTaskCommentCommandValidator : AbstractValidator<CreateTaskCommentCommand>
{
    public CreateTaskCommentCommandValidator()
    {
        RuleFor(c => c.UserGid).NotEmpty();
        RuleFor(c => c.TaskGid).NotEmpty();
        RuleFor(c => c.Comment).NotEmpty().MaximumLength(250);
    }
}