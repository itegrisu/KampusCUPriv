using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelGraduatedSchools.Commands.UploadFile
{
    public class UploadGraduatedSchoolFileCommand : IRequest<UploadGraduatedSchoolFileResponse>
    {
        public Guid Gid { get; set; }
        public string FileName { get; set; }
    }
}
