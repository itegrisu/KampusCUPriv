using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelDocuments.Commands.UploadFile
{
    public class UploadPersonnelFileCommand :IRequest<UploadPersonnelFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
