using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(c => c.RefreshToken)
                .NotNull()
                .NotEmpty()
                .WithMessage("Refresh token boş olamaz.");
        }
    }
}
