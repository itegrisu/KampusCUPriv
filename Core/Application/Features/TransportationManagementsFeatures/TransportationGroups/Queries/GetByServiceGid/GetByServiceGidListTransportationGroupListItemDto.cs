using Core.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TransportationManagementsFeatures.TransportationGroups.Queries.GetByServiceGid
{
    public class GetByServiceGidListTransportationGroupListItemDto : IDto
    {
        public Guid Gid { get; set; }
        public Guid GidTransportationServiceFK { get; set; }
        public string TransportationServiceFKServiceNo { get; set; }
        public Guid GidStartCountryFK { get; set; }
        public string StartCountryFKName { get; set; }
        public Guid GidStartCityFK { get; set; }
        public string StartCityFKName { get; set; }
        public Guid GidStartDistrictFK { get; set; }
        public string StartDistrictFKDistrictName { get; set; }
        public Guid GidEndCountryFK { get; set; }
        public string EndCountryFKName { get; set; }
        public Guid GidEndCityFK { get; set; }
        public string EndCityFKName { get; set; }
        public Guid GidEndDistrictFK { get; set; }
        public string EndDistrictFKDistrictName { get; set; }
        public string GroupName { get; set; }
        public decimal TransportationFee { get; set; }
        public string StartPlace { get; set; }
        public string EndPlace { get; set; }
        public string? Description { get; set; }
        public int? PassengerCount { get; set; }
        public string? RefNoTransportationGroup { get; set; }
    }
}
