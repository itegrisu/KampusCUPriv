using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrganizationManagementFeatures.OrganizationFiles.Queries.GetByOrganizationGid
{
    public class GetByOrganizationGidListOrganizationFileListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidOrganizationFK { get; set; }
        public string OrganizationFKOrganizationName { get; set; }
        public string Title { get; set; }
        public string? Document { get; set; }
        public string? Description { get; set; }
        public int RowNo { get; set; }
    }
}
