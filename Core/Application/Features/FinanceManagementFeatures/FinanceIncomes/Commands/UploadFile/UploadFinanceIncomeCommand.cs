using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinanceManagementFeatures.FinanceIncomes.Commands.UploadFile
{
    public class UploadFinanceIncomeCommand : IRequest<UploadFinanceIncomeResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }    
}
