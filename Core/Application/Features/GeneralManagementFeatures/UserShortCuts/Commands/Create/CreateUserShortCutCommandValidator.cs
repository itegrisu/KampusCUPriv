using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.UserShortCuts.Commands.Create;

public class CreateUserShortCutCommandValidator : AbstractValidator<CreateUserShortCutCommand>
{
    public CreateUserShortCutCommandValidator()
    {
        RuleFor(c => c.GidUserFK).NotNull().NotEmpty();
        RuleFor(c => c.PageName).NotNull().NotEmpty().MaximumLength(100);
        RuleFor(c => c.PageUrl).NotNull().NotEmpty().MaximumLength(150);

    }
}