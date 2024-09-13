using FluentValidation;

namespace Application.Features.AuthManagementFeatures.AuthPages.Commands.Update;

public class UpdateAuthPageCommandValidator : AbstractValidator<UpdateAuthPageCommand>
{
    public UpdateAuthPageCommandValidator()
    {
        RuleFor(c => c.Gid).NotEmpty();
        RuleFor(c => c.PageName).NotEmpty().MaximumLength(100);
        RuleFor(c => c.RedirectName).NotEmpty().MaximumLength(150);
        RuleFor(c => c.PhysicalFilePath).NotEmpty().MaximumLength(250);
        RuleFor(c => c.MenuLink).MaximumLength(150);
        RuleFor(c => c.PathForAuthCheck).MaximumLength(250);
        RuleFor(c => c.IsShowMenu).NotEmpty();
        //RuleFor(c => c.HelpFileName).NotEmpty();
      }
}