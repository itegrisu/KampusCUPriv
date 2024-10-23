using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Commands.UploadFile
{
    public class UploadPermitInfoFileCommand :IRequest<UploadPermitInfoFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
