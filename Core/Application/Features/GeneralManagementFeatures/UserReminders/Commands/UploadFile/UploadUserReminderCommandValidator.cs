using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.UserReminders.Commands.UploadFile
{
    public class UploadUserReminderCommandValidator : AbstractValidator<UploadUserReminderCommand>
    {
        public UploadUserReminderCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
