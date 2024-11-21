using Core.Entities;
using Domain.Entities.PersonnelManagements;
using Domain.Entities.SupplierCustomerManagements;
using Domain.Entities.TransportationManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class City : BaseEntity
    {

        public Guid GidCountryFK { get; set; }
        public Country CountryFK { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? PlateCode { get; set; }

        public ICollection<PersonnelAddress>? PersonnelAddresses { get; set; }
        public ICollection<SCAddress>? SCAddresses { get; set; }
        public ICollection<TransportationGroup>? StartTransportationGroups { get; set; }
        public ICollection<District>? Districts { get; set; }
        public ICollection<TransportationGroup>? EndTransportationGroups { get; set; }
    }
}