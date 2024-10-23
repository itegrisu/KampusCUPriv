using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPassportInfos.Commands.UploadFile
{
    public class UploadPassportFileCommand : IRequest<UploadPassportFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
