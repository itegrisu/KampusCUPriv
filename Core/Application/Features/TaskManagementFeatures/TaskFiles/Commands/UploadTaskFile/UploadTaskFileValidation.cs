using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.UploadTaskFile
{
    public class UploadTaskFileValidation : AbstractValidator<UploadTaskFileCommand>
    {
        public UploadTaskFileValidation()
        {
            RuleFor(c => c.Gid).NotEmpty();
            RuleFor(c => c.FileName).NotEmpty();
        }
    }
}
