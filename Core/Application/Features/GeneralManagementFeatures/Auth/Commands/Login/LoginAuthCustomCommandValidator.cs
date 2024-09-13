using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.Login
{
    public class LoginAuthCustomCommandValidator : AbstractValidator<LoginAuthCommand>
    {
        public LoginAuthCustomCommandValidator()
        {
            RuleFor(c => c.Email).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }
}