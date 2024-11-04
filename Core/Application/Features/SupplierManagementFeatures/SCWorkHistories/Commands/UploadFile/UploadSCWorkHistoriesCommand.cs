using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SupplierManagementFeatures.SCWorkHistories.Commands.UploadFile
{
    public class UploadSCWorkHistoriesCommand : IRequest<UploadSCWorkHistoriesResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
