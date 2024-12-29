using Core.Entities;
using Domain.Entities.AccommodationManagements;
using Domain.Entities.GeneralManagements;
using Domain.Entities.TransportationManagements;

namespace Domain.Entities.DefinitionManagements
{
    public class Country : BaseEntity, IHasRowNo
    {

        public string Name { get; set; } = string.Empty;
        public string CountryCode { get; set; } = string.Empty;
        public string? PhoneCode { get; set; }
        public int RowNo { get; set; }

        public ICollection<User>? Users { get; set; }
        public ICollection<City>? Cities { get; set; }
        public ICollection<TransportationGroup>? StartTransportationGroups { get; set; }
        public ICollection<TransportationGroup>? EndTransportationGroups { get; set; }
        public ICollection<Guest>? Guests { get; set; }
        public ICollection<GuestAccommodationPerson>? GuestAccommodationPersons { get; set; }
    }
}
