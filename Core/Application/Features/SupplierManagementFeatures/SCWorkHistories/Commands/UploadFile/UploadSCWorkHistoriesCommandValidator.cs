using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SupplierManagementFeatures.SCWorkHistories.Commands.UploadFile
{
    public class UploadSCWorkHistoriesCommandValidator : AbstractValidator<UploadSCWorkHistoriesCommand>
    {
        public UploadSCWorkHistoriesCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();

        }
    }
}
