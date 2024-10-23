using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelResidenceInfos.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelResidenceInfoListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public string SessionSerialNo { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime ValidityDate { get; set; }
        public string? Document { get; set; }
    }
}
