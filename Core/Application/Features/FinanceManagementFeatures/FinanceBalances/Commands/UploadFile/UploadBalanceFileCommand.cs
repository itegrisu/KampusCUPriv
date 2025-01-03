using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FinanceManagementFeatures.FinanceBalances.Commands.UploadFile
{
    public class UploadBalanceFileCommand : IRequest<UploadBalanceFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
