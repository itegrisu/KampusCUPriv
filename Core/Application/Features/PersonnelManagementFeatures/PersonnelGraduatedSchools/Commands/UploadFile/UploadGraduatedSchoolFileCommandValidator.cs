using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.UploadFile
{
    public class UploadGraduatedSchoolFileCommandValidator : AbstractValidator<UploadGraduatedSchoolFileCommand>
    {
        public UploadGraduatedSchoolFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
