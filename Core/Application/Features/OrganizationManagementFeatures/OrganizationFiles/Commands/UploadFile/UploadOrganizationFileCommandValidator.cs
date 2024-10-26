using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.UploadFile
{
    public class UploadOrganizationFileCommandValidator : AbstractValidator<UploadOrganizationFileCommand>
    {
        public UploadOrganizationFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
