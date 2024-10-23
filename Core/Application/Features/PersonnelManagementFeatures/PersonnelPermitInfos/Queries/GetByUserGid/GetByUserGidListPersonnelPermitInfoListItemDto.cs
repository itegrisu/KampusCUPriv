using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelPermitInfos.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelPermitInfoListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public Guid GidPermitFK { get; set; }
        public string PermitTypeFKName { get; set; }
        public DateTime PermitStartDate { get; set; }
        public DateTime PermitEndDate { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }
    }
}
