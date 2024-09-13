using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthManagementFeatures.AuthUserRoles.Queries.GetList
{
    public class GetListAuthUserRoleListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidUserFK { get; set; }
        public Guid GidRoleFK { get; set; }
        public Guid GidPageFK { get; set; }
        public int RowNo { get; set; }
        public string UserName { get; set; }
        public string? AuthRoleFKRoleName { get; set; }
        public string? AuthPageFKPageName { get; set; }
    }
}
