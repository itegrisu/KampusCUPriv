using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.UploadFile
{
    public class UploadPassportFileCommandValidator : AbstractValidator<UploadPassportFileCommand>
    {
        public UploadPassportFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
