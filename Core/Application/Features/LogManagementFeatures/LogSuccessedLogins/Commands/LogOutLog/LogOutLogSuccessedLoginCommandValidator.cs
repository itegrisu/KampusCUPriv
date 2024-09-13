using Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LogManagementFeatures.LogSuccessedLogins.Commands.LogOutLog
{
    public class LogOutLogSuccessedLoginCommandValidator : AbstractValidator<LogOutLogSuccessedLoginCommand>
    {
        public LogOutLogSuccessedLoginCommandValidator()
        {
            RuleFor(c => c.SucceededLogGid).NotNull().NotEmpty();
            RuleFor(c => c.SessionId).NotNull().NotEmpty().MaximumLength(100);
        }
    }
}
