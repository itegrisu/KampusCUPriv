using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.UploadFile
{
    public class UploadPermitInfoFileCommandValidator : AbstractValidator<UploadPermitInfoFileCommand>
    {
        public UploadPermitInfoFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
