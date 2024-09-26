using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TaskManagementFeatures.TaskFiles.Commands.UploadTaskFileTemp
{
    public class UploadTaskFileTempValidation : AbstractValidator<UploadTaskFileTempCommand>
    {
        public UploadTaskFileTempValidation()
        {
            RuleFor(c => c.Params).NotEmpty();
        }
    }
}
