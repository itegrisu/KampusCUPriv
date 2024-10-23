using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PersonnelManagementFeatures.PersonnelAddresses.Queries.GetByUserGid
{
    public class GetByUserGidListPersonnelAddressListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidPersonnelFK { get; set; }
        public string UserFKFullName { get; set; }
        public Guid GidCityFK { get; set; }
        public string CityFKName { get; set; }
        public string AddressTitle { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
    }
}
