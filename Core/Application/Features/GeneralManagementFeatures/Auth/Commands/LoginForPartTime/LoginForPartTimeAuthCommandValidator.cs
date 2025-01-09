using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.LoginForPartTime
{
    public class LoginForPartTimeAuthCommandValidator : AbstractValidator<LoginForPartTimeAuthCommand>
    {
        public LoginForPartTimeAuthCommandValidator()
        {
            RuleFor(c => c.Username).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }
}
