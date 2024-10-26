using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Commands.UploadFile
{
    public class UploadOrganizationFileCommand : IRequest<UploadOrganizationFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }    
}
