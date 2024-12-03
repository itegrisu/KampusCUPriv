using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.FinanceManagements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.TransportationManagements
{
    public class TransportationGroup : BaseEntity
    {
        public Guid GidTransportationServiceFK { get; set; }
        public TransportationService TransportationServiceFK { get; set; }
        public Guid GidStartCountryFK { get; set; }
        public Country StartCountryFK { get; set; }
        public Guid GidStartCityFK { get; set; }
        public City StartCityFK { get; set; }
        public Guid GidStartDistrictFK { get; set; }
        public District StartDistrictFK { get; set; }
        public Guid GidEndCountryFK { get; set; }
        public Country EndCountryFK { get; set; }
        public Guid GidEndCityFK { get; set; }
        public City EndCityFK { get; set; }
        public Guid GidEndDistrictFK { get; set; }
        public District EndDistrictFK { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public decimal TransportationFee { get; set; }
        public string StartPlace { get; set; } = string.Empty;
        public string EndPlace { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? RefNoTransportationGroup { get; set; }
        public ICollection<TransportationPassenger>? TransportationPassengers { get; set; }
    }
}
