using Core.Application.Dtos;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SupplierManagementFeatures.SCPersonnels.Queries.GetByCompanyGid
{
    public class GetByCompanyGidListSCPersonnelListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidSCCompanyFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public string UserFKAvatar { get; set; }
        public EnumSCPersonnelLoginStatus SCPersonnelLoginStatus { get; set; }
    }
}
