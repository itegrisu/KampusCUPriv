using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskGroupUsers.Commands.Create;

public class CreateTaskGroupUserCommandValidator : AbstractValidator<CreateTaskGroupUserCommand>
{
    public CreateTaskGroupUserCommandValidator()
    {
        RuleFor(c => c.UserGid).NotEmpty();
        RuleFor(c => c.TaskGroupGid).NotEmpty();
    }
}