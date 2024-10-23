using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.UploadFile
{
    public class UploadPersonnelFileCommandValidator : AbstractValidator<UploadPersonnelFileCommand>
    {
        public UploadPersonnelFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
