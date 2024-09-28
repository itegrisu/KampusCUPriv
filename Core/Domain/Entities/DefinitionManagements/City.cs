using Core.Entities;
using Domain.Entities.PersonnelManagements;
using Domain.Entities.SupplierCustomerManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class City : BaseEntity
    {

        public Guid GidUlkeFK { get; set; }
        public Country CountryFK { get; set; }

        public string SehirAdi { get; set; } = string.Empty;
        public string? PlakaKodu { get; set; }

        public ICollection<PersonnelAddress>? PersonnelAddresses { get; set; }
        public ICollection<SCAddress>? SCAddresses { get; set; }


    }
}