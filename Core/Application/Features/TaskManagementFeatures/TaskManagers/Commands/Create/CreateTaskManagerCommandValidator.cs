using FluentValidation;

namespace Application.Features.TaskManagementFeatures.TaskManagers.Commands.Create;

public class CreateTaskManagerCommandValidator : AbstractValidator<CreateTaskManagerCommand>
{
    public CreateTaskManagerCommandValidator()
    {
        RuleFor(c => c.UserGid).NotEmpty();
    }
}