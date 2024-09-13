using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthPages.Commands.Create;

public class CreateAuthPageCommandValidator : AbstractValidator<CreateAuthPageCommand>
{
    public CreateAuthPageCommandValidator()
    {
        RuleFor(c => c.PageName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.RedirectName).NotEmpty().MaximumLength(150);
        RuleFor(c => c.PhysicalFilePath).NotEmpty().MaximumLength(250);
        RuleFor(c => c.MenuLink).MaximumLength(150);
        RuleFor(c => c.PathForAuthCheck).MaximumLength(250);
        RuleFor(c => c.IsShowMenu).NotEmpty();
        //RuleFor(c => c.HelpFileName).NotEmpty();
    }
}