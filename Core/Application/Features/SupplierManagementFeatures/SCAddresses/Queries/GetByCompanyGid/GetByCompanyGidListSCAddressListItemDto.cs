using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SupplierManagementFeatures.SCAddresses.Queries.GetByCompanyGid
{
    public class GetByCompanyGidListSCAddressListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidSCCompanyFK { get; set; }
        public string SCCompanyFKCompanyName { get; set; }
        public Guid GidCityFK { get; set; }
        public string CityFKName { get; set; }
        public string Title { get; set; }
        public string? District { get; set; }
        public string? PostalCode { get; set; }
        public string Address { get; set; }
    }
}
