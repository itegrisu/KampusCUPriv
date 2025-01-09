using FluentValidation;

namespace Application.Features.GeneralManagementFeatures.Auth.Commands.LoginForWorker
{
    public class LoginForWorkerAuthCommandValidator : AbstractValidator<LoginForWorkerAuthCommand>
    {
        public LoginForWorkerAuthCommandValidator()
        {
            RuleFor(c => c.Phone).NotEmpty();
            RuleFor(c => c.Password).NotEmpty();
        }
    }
}
