using Core.Entities;
using Domain.Entities.DefinitionManagements;
using Domain.Entities.GeneralManagements;

namespace Domain.Entities.PersonnelManagements
{
    public class PersonnelAddress : BaseEntity
    {
        public Guid GidPersonnelFK { get; set; }
        public User UserFK { get; set; }
        public Guid GidCityFK { get; set; }
        public City CityFK { get; set; }
        public string AddressTitle { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Description { get; set; }


    }
}
