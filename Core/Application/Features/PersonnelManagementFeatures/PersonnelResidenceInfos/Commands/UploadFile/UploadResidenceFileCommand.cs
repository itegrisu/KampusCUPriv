using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Commands.UploadFile
{
    public class UploadResidenceFileCommand : IRequest<UploadResidenceFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }

}
