using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandValidator : AbstractValidator<RevokeRefreshTokenCommand>
    {
        public RevokeRefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh token boş olamaz.")
                .NotNull()
                .WithMessage("Refresh token boş olamaz.");
        }
    }
}
