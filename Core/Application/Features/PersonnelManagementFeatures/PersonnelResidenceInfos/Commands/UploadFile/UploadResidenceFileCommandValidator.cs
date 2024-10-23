using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.UploadFile
{
    public class UploadResidenceFileCommandValidator : AbstractValidator<UploadResidenceFileCommand>
    {
        public UploadResidenceFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
