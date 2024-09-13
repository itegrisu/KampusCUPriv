using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.LoginWithSystemAdmin
{
    public class LoginAuthWithSystemAdminValidator : AbstractValidator<LoginAuthWithSystemAdminCommand>
    {
        public LoginAuthWithSystemAdminValidator()
        {
            RuleFor(c => c.Gid).NotEmpty().NotNull();
            RuleFor(c => c.GidUser).NotEmpty().NotNull();
        }
    }
}