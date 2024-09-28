using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Create;

public class CreateUserModuleAuthCommandValidator : AbstractValidator<CreateUserModuleAuthCommand>
{
    public CreateUserModuleAuthCommandValidator()
    {
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();

RuleFor(c => c.ModuleType).NotNull().NotEmpty();


    }
}