using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrganizationManagementFeatures.OrganizationGroups.Queries.GetByOrganizationGid
{
    public class GetByOrganizationGidListOrganizationGroupListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidOrganizationFK { get; set; }
        public string OrganizationFKOrganizationName { get; set; }
        public string GroupName { get; set; }
        public int RowNo { get; set; }
    }
}
