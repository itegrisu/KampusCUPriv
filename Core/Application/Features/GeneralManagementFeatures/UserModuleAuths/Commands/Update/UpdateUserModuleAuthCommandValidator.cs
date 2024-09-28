using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.UserModuleAuths.Commands.Update;

public class UpdateUserModuleAuthCommandValidator : AbstractValidator<UpdateUserModuleAuthCommand>
{
    public UpdateUserModuleAuthCommandValidator()
    {
        RuleFor(c => c.Gid).NotNull().NotEmpty();
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();

RuleFor(c => c.ModuleType).NotNull().NotEmpty();


    }
}