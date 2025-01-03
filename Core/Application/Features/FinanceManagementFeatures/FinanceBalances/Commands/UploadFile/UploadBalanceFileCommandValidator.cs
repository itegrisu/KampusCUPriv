using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.UploadFile
{
    public class UploadBalanceFileCommandValidator : AbstractValidator<UploadBalanceFileCommand>
    {
        public UploadBalanceFileCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
