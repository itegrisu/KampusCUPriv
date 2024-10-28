using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinanceManagementFeatures.FinanceExpenses.Commands.FileUpload
{
    public class UploadFinanceExpenseCommandValidator : AbstractValidator<UploadFinanceExpenseCommand>
    {
        public UploadFinanceExpenseCommandValidator()
        {
            RuleFor(c => c.Gid).NotEmpty();
        }
    }
}
