using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DefinitionManagementFeatures.Districts.Queries.GetByCityGid
{
    public class GetByCityGidListDistrictListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidCityFK { get; set; }
        public string CityFKName { get; set; }
        public int DistrictCode { get; set; }
        public string DistrictName { get; set; }
    }
}
