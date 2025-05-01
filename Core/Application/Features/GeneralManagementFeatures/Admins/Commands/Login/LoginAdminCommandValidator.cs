using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Admins.Commands.Login
{
    public class LoginAdminCommandValidator : AbstractValidator<LoginAdminCommand>
    {
        public LoginAdminCommandValidator()
        {
            RuleFor(c => c.Email).NotNull().NotEmpty();
            RuleFor(c => c.Password).NotNull().NotEmpty();
        }
    }
}
