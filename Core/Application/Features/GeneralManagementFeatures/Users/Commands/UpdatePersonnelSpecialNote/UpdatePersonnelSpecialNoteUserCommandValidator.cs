using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Users.Commands.UpdatePersonnelSpecialNote
{
    public class UpdatePersonnelSpecialNoteUserCommandValidator : AbstractValidator<UpdatePersonnelSpecialNoteUserCommand>
    {
        public UpdatePersonnelSpecialNoteUserCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
