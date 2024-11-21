using Core.Entities;
using Domain.Entities.TransportationManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DefinitionManagements
{
    public class District : BaseEntity
    {
        public Guid GidCityFK { get; set; }
        public City CityFK { get; set; }

        public int DistrictCode { get; set; }
        public string DistrictName { get; set; } = string.Empty;

        public ICollection<TransportationGroup>? StartTransportationGroups { get; set; }
        public ICollection<TransportationGroup>? EndTransportationGroups { get; set; }
    }
}
