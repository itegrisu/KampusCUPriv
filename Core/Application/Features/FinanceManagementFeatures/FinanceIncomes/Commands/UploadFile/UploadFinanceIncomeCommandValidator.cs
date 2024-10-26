using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.UploadFile
{
    public class UploadFinanceIncomeCommandValidator : AbstractValidator<UploadFinanceIncomeCommand>
    {
        public UploadFinanceIncomeCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
