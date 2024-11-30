using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DefinitionManagementFeatures.Cities.Queries.GetByCountryGid
{
    public class GetByCoıntryGidListCityListItemDto :IDto
    {
        public Guid Gid { get; set; }
        public Guid GidCountryFK { get; set; }
        public string CountryFKName { get; set; }
        public string Name { get; set; }
        public string? PlateCode { get; set; }
    }
}
