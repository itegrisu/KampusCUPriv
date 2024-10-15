using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.GeneralManagementFeatures.Departments.Queries.GetListWithUser
{
    public class GetListWithUserDepartmentListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidMainAdminFK { get; set; }
        public string MainAdminFKFullName { get; set; }
        public Guid? GidCoAdminFK { get; set; }
        public string CoAdminFKFullName { get; set; }
        public string Name { get; set; }
        public string? Details { get; set; }
        public int UserCount { get; set; }
    }
}
