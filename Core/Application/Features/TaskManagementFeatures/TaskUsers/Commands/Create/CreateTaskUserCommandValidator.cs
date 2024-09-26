using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskUsers.Commands.Create;

public class CreateTaskUserCommandValidator : AbstractValidator<CreateTaskUserCommand>
{
    public CreateTaskUserCommandValidator()
    {
        RuleFor(c => c.TaskGid).NotEmpty();
        RuleFor(c => c.UserGid).NotEmpty();
    }
}