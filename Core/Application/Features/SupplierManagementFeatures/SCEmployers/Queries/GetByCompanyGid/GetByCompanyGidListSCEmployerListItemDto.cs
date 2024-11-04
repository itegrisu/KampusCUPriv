using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SupplierManagementFeatures.SCEmployers.Queries.GetByCompanyGid
{
    public class GetByCompanyGidListSCEmployerListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidSCCompanyFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public string FullName { get; set; }
        public string Duty { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? SpecialNote { get; set; }
    }
}
