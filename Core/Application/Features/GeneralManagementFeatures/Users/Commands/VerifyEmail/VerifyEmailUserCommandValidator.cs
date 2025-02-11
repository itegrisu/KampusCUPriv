using Application.Features.GeneralManagementFeatures.Users.Commands.Login;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.VerifyEmail
{
    public class VerifyEmailUserCommandValidator : AbstractValidator<VerifyEmailUserCommand>
    {
        public VerifyEmailUserCommandValidator()
        {
            RuleFor(c => c.Email).NotNull().NotEmpty();
            RuleFor(c => c.VerificationCode).NotNull().NotEmpty();
        }
    }
}
